using Glimpse.Core.Framework.Support;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;

namespace Glimpse.NHibernate.Inspector.Core
{
    internal class NHibernateExecutionBlock 
        : ExecutionBlockBase
    {
        public static readonly NHibernateExecutionBlock Instance = new NHibernateExecutionBlock();

        private NHibernateExecutionBlock()
        {
            INHibernateProvider nhibernateProvider = new NHibernateProvider();
            IGlimpseDbDriverFactory glimpseDbDriverFactory = new GlimpseDbDriverFactory();
            IGlimpseDbDriverActivator glimpseDbDriverActivator = new GlimpseDbDriverActivator();
            RegisterProvider(new NHibernateDbDriverWrapperExecutionTask(Logger, nhibernateProvider, glimpseDbDriverFactory, glimpseDbDriverActivator));
        }
    }
}
