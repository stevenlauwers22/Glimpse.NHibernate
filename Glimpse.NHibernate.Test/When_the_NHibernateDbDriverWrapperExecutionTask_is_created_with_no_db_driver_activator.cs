using System;
using Glimpse.Core.Extensibility;
using Glimpse.NHibernate.Inspector.Core;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Moq;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_created_with_no_db_driver_activator
    {
        [Test]
        public void It_should_not_throw_an_exception()
        {
            // Arrange
            var logger = new Mock<ILogger>();
            var nhibernateProvider = new Mock<INHibernateProvider>();
            var glimpseDbDriverFactory = new Mock<IGlimpseDbDriverFactory>();
            var createAction = new Action(() => new NHibernateDbDriverWrapperExecutionTask(logger.Object, nhibernateProvider.Object, glimpseDbDriverFactory.Object, null));

            try
            {
                // Act
                createAction();
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual(exception.ParamName, "glimpseDbDriverActivator");
                Assert.Pass();
            }

            Assert.Fail("Expected exception as not thrown");
        }
    }
}