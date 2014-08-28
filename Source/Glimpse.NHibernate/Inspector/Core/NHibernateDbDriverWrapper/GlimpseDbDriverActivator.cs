using System;
using Glimpse.NHibernate.AlternateType;

namespace Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper
{
    public interface IGlimpseDbDriverActivator
    {
        IGlimpseDbDriver CreateDbDriver(Type dbDriverType);
    }

    public class GlimpseDbDriverActivator
        : IGlimpseDbDriverActivator
    {
        public IGlimpseDbDriver CreateDbDriver(Type dbDriverType)
        {
            if (dbDriverType == null)
                throw new ArgumentNullException("dbDriverType");

            var dbConstructor = dbDriverType.GetConstructor(Type.EmptyTypes);
            if (dbConstructor == null)
                throw new InvalidOperationException(string.Format("{0} should have a parameterless constructor", dbDriverType));

            var dbDriver = (IGlimpseDbDriver)dbConstructor.Invoke(null);
            return dbDriver;
        }
    }
}