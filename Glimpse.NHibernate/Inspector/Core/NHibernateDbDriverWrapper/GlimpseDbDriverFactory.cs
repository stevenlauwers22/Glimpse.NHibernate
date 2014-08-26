using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using Glimpse.Core.Framework;
using Microsoft.CSharp;

namespace Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper
{
    public interface IGlimpseDbDriverFactory
    {
        Type GetDbDriverType(Assembly nhibernateAssembly);
    }

    public class GlimpseDbDriverFactory 
        : IGlimpseDbDriverFactory
    {
        public Type GetDbDriverType(Assembly nhibernateAssembly)
        {
            // Validate
            if (nhibernateAssembly == null)
                return null;

            // Determine the appropriate glimpse driver version
            var version = nhibernateAssembly.GetName().Version;
            var versionNumber = string.Format("{0}{1}{2}{3}", version.Major, version.Minor, version.Build, version.Revision);
            var driver = string.Format("Glimpse.NHibernate.AlternateType.GlimpseDbDriverNh{0}", versionNumber);

            // Get the glimpse driver code
            var code = GetEmbeddedResource(GetType().Assembly, string.Format("{0}.cs", driver));
            if (string.IsNullOrEmpty(code))
                return null;

            // Compile the glimpse driver code
            var assembliesToReference = new[] { nhibernateAssembly, typeof(DbConnection).Assembly, typeof(TypeConverter).Assembly, typeof(GlimpseConfiguration).Assembly, typeof(Ado.Initialize).Assembly, GetType().Assembly };
            var generatedAssembly = CreateAssembly(code, assembliesToReference);
            var generatedType = generatedAssembly.GetType(driver);
            return generatedType;
        }

        private static string GetEmbeddedResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    return null;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static Assembly CreateAssembly(string code, IEnumerable<Assembly> referenceAssemblies)
        {
            //See http://stackoverflow.com/questions/3032391/csharpcodeprovider-doesnt-return-compiler-warnings-when-there-are-no-errors
            var provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
            var compilerParameters = new CompilerParameters { GenerateExecutable = false, GenerateInMemory = true };
            compilerParameters.ReferencedAssemblies.AddRange(referenceAssemblies.Select(a => a.Location).ToArray());

            var results = provider.CompileAssemblyFromSource(compilerParameters, code);
            if (results.Errors.HasErrors)
                throw new InvalidOperationException(results.Errors[0].ErrorText);

            return results.CompiledAssembly;
        }
    }
}