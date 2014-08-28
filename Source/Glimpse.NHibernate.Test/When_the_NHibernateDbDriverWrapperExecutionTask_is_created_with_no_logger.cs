using System;
using Glimpse.NHibernate.Inspector.Core;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Moq;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_created_with_no_logger
    {
        [Test]
        public void It_should_not_throw_an_exception()
        {
            // Arrange
            var nhibernateProvider = new Mock<INHibernateProvider>();
            var glimpseDbDriverFactory = new Mock<IGlimpseDbDriverFactory>();
            var glimpseDbDriverActivator = new Mock<IGlimpseDbDriverActivator>();
            var createAction = new Action(() => new NHibernateDbDriverWrapperExecutionTask(null, nhibernateProvider.Object, glimpseDbDriverFactory.Object, glimpseDbDriverActivator.Object));

            try
            {
                // Act
                createAction();
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual(exception.ParamName, "logger");
                Assert.Pass();
            }

            Assert.Fail("Expected exception as not thrown");
        }
    }
}