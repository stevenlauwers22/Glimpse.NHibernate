using System;
using Glimpse.NHibernate.Inspector.Core;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Moq;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_created_with_no_logger
    {
        [Fact]
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
                Assert.False(true, "Expected exception as not thrown");
            }
            catch (ArgumentNullException exception)
            {
                // Assert
                Assert.Equal(exception.ParamName, "logger");
            }
        }
    }
}