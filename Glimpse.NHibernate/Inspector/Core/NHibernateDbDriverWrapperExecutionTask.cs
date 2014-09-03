using System;
using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Framework.Support;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;

namespace Glimpse.NHibernate.Inspector.Core
{
    public class NHibernateDbDriverWrapperExecutionTask 
        : IExecutionTask
    {
        private readonly ILogger _logger;
        private readonly INHibernateProvider _nhibernateProvider;
        private readonly IGlimpseDbDriverFactory _glimpseDbDriverFactory;
        private readonly IGlimpseDbDriverActivator _glimpseDbDriverActivator;

        public NHibernateDbDriverWrapperExecutionTask(
            ILogger logger, 
            INHibernateProvider nhibernateProvider, 
            IGlimpseDbDriverFactory glimpseDbDriverFactory, 
            IGlimpseDbDriverActivator glimpseDbDriverActivator)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            if (nhibernateProvider == null)
                throw new ArgumentNullException("nhibernateProvider");

            if (glimpseDbDriverFactory == null)
                throw new ArgumentNullException("glimpseDbDriverFactory");

            if (glimpseDbDriverActivator == null)
                throw new ArgumentNullException("glimpseDbDriverActivator");

            _logger = logger;
            _nhibernateProvider = nhibernateProvider;
            _glimpseDbDriverFactory = glimpseDbDriverFactory;
            _glimpseDbDriverActivator = glimpseDbDriverActivator;
        }

        public void Execute()
        {
            _logger.Info("NHibernateInspector: Started wrapping the NHibernate DbDriver");

            var nhibernateDriverInfos = _nhibernateProvider.GetNHibernateDriverInfos();
            if (nhibernateDriverInfos == null)
                return;

            var nhibernateDriverInfosList = nhibernateDriverInfos.ToList();
            var nhibernateAssembly = _nhibernateProvider.GetNhibernateAssembly();

            _logger.Info(string.Format("NHibernateInspector: Found {0} drivers to wrap", nhibernateDriverInfosList.Count));

            foreach (var nhibernateDriverInfo in nhibernateDriverInfosList)
            {
                if (nhibernateDriverInfo == null)
                    continue;

                // Check if the driver is already wrapped
                if (nhibernateDriverInfo.IsWrapped())
                    continue;

                // Get the glimpse driver
                var glimpseDbDriverType = _glimpseDbDriverFactory.GetDbDriverType(nhibernateAssembly);
                if (glimpseDbDriverType == null)
                    continue;

                var glimpseDbDriver = _glimpseDbDriverActivator.CreateDbDriver(glimpseDbDriverType);
                if (glimpseDbDriver == null)
                    continue;

                // Wrap the driver with the glimpse driver
                var driver = nhibernateDriverInfo.GetDriver();
                glimpseDbDriver.Wrap(driver);

                // Inject the glimpse driver into nhibernate
                nhibernateDriverInfo.SetDriver(glimpseDbDriver);

                _logger.Info(string.Format("NHibernateInspector: Wrapped a driver ... " + Environment.NewLine +
                                           "Original driver type: {0}" + Environment.NewLine +
                                           "Glimpse driver type: {1}", driver.GetType().FullName, glimpseDbDriver.GetType().FullName));
            }

            _logger.Info("NHibernateInspector: Finished wrapping the NHibernate DbDriver");
        }
    }
}