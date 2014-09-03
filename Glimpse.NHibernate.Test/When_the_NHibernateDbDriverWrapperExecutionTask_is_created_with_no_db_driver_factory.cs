using System;
using Glimpse.Core.Extensibility;
using Glimpse.NHibernate.Inspector.Core;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Moq;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_created_with_no_db_driver_factory
    {
        [Fact]
        public void It_should_not_throw_an_exception()
        {
            // Arrange
            var logger = new Mock<ILogger>();
            var nhibernateProvider = new Mock<INHibernateProvider>();
            var glimpseDbDriverActivator = new Mock<IGlimpseDbDriverActivator>();
            var createAction = new Action(() => new NHibernateDbDriverWrapperExecutionTask(logger.Object, nhibernateProvider.Object, null, glimpseDbDriverActivator.Object));

            try
            {
                // Act
                createAction();
                Assert.False(true, "Expected exception as not thrown");
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal(exception.ParamName, "glimpseDbDriverFactory");
            }
        }
    }
}