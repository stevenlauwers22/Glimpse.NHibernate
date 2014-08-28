using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.NHibernate.Inspector.Core;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Moq;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public abstract class When_the_NHibernateDbDriverWrapperExecutionTask_is_told_to_Execute
    {
        protected abstract void BuildSessionFactory();

        [Test]
        public void It_should_wrap_all_drivers()
        {
            var logger = new Mock<ILogger>();
            var nhibernateProvider = new NHibernateProvider();
            var glimpseDbDriverFactory = new GlimpseDbDriverFactory();
            var glimpseDbDriverActivator = new GlimpseDbDriverActivator();
            var nhibernateDbDriverWrapperExecutionTask = new NHibernateDbDriverWrapperExecutionTask(logger.Object, nhibernateProvider, glimpseDbDriverFactory, glimpseDbDriverActivator);
            BuildSessionFactory();

            // Act
            nhibernateDbDriverWrapperExecutionTask.Execute();

            // Assert
            var nhibernateDriverInfos = nhibernateProvider.GetNHibernateDriverInfos().ToList();
            Assert.AreEqual(1, nhibernateDriverInfos.Count());

            foreach (var nhibernateDriverInfo in nhibernateDriverInfos)
            {
                Assert.IsTrue(nhibernateDriverInfo.IsWrapped());
            }
        }
    }
}