using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;
using DotNetBuild.Tasks.NuGet;

var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.AddTarget("ci", "Continuous integration target", c 
    => c.DependsOn("updateVersionNumber")
        .And("buildRelease")
        .And("runTests")
        .And("createPackage")
);

dotNetBuild.AddTarget("updateVersionNumber", "Update version number", c 
    => c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            const String assemblyMajorVersion = "1";
            const String assemblyMinorVersion = "0";
            const String assemblyBuildNumber = "0";
            var assemblyInfoTask = new AssemblyInfo
            {
                AssemblyInfoFiles = new[]
                {
                    Path.Combine(solutionDirectory, @"Glimpse.NHibernate\Properties\AssemblyInfo.cs")
                },
                AssemblyInformationalVersion = String.Format("{0}.{1}.{2}", assemblyMajorVersion, assemblyMinorVersion, assemblyBuildNumber),
                UpdateAssemblyInformationalVersion = true,
                AssemblyMajorVersion = assemblyMajorVersion,
                AssemblyMinorVersion = assemblyMinorVersion,
                AssemblyBuildNumber = assemblyBuildNumber,
                AssemblyRevision = "0",
                AssemblyFileMajorVersion = assemblyMajorVersion,
                AssemblyFileMinorVersion = assemblyMinorVersion,
                AssemblyFileBuildNumber = assemblyBuildNumber,
                AssemblyFileRevision = "0"
            };

            var result = assemblyInfoTask.Execute();
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly version: " + assemblyInfoTask.MaxAssemblyVersion);
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly informational version: " + assemblyInfoTask.AssemblyInformationalVersion);
            context.FacilityProvider.Get<IStateWriter>().Add("VersionNumber", assemblyInfoTask.AssemblyInformationalVersion);

            return result;
		}));

dotNetBuild.AddTarget("buildRelease", "Build in release mode", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
			var msBuildTask = new MsBuildTask
			{
				Project = Path.Combine(solutionDirectory, "Glimpse.NHibernate.sln"),
				Target = "Rebuild",
				Parameters = "Configuration=Release"
			};

			return msBuildTask.Execute();
		}));

dotNetBuild.AddTarget("runTests", "Run tests", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var xunitExe = context.ConfigurationSettings.Get<String>("PathToXUnitRunnerExe");
            var xunitAssemblies = new List<string>
            {
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test\bin\Release\Glimpse.NHibernate.Test.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh1214000\bin\Release\Glimpse.NHibernate.Test.Nh1214000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh2014000\bin\Release\Glimpse.NHibernate.Test.Nh2014000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh2104000\bin\Release\Glimpse.NHibernate.Test.Nh2104000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh2124000\bin\Release\Glimpse.NHibernate.Test.Nh2124000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3004000\bin\Release\Glimpse.NHibernate.Test.Nh3004000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3104000\bin\Release\Glimpse.NHibernate.Test.Nh3104000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3204000\bin\Release\Glimpse.NHibernate.Test.Nh3204000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3304000\bin\Release\Glimpse.NHibernate.Test.Nh3304000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3314000\bin\Release\Glimpse.NHibernate.Test.Nh3314000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3324000\bin\Release\Glimpse.NHibernate.Test.Nh3324000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3334000\bin\Release\Glimpse.NHibernate.Test.Nh3334000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3334001\bin\Release\Glimpse.NHibernate.Test.Nh3334001.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3344000\bin\Release\Glimpse.NHibernate.Test.Nh3344000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh3404000\bin\Release\Glimpse.NHibernate.Test.Nh3404000.dll"),
                Path.Combine(solutionDirectory, @"Glimpse.NHibernate.Test.Nh4004000\bin\Release\Glimpse.NHibernate.Test.Nh4004000.dll")
            };
            
            foreach (var xunitAssembly in xunitAssemblies)
            {
                var xunitTask = new XunitTask
                {
                    XunitExe = Path.Combine(solutionDirectory, xunitExe),
                    Assembly = xunitAssembly
                };
            
                var xunitResult = xunitTask.Execute();
                if (xunitResult == false)
                    return false;
            }
            
            return true;
		}));

dotNetBuild.AddTarget("createPackage", "Create NuGet package", c 
    => c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuSpecFile = Path.Combine(solutionDirectory, @"Glimpse.NHibernate\package.nuspec"),
                OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddTarget("deploy", "Deploy to NuGet", c
	=> c.DependsOn("publishPackage"));

dotNetBuild.AddTarget("publishCorePackage", "Publish NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\Glimpse.NHibernate.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddConfiguration("defaultConfig", c 
	=> c.AddSetting("SolutionDirectory", @"..\")
        .AddSetting("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe")
        .AddSetting("NuGetApiKey", "")
        .AddSetting("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe")
);

dotNetBuild.RunFromScriptArguments();