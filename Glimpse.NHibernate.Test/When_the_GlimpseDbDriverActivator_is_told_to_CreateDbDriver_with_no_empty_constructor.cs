using System;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_GlimpseDbDriverActivator_is_told_to_CreateDbDriver_with_no_empty_constructor
    {
        [Fact]
        public void It_should_throw_an_invalid_operation_exception()
        {
            // Arrange
            var dbDriverType = typeof(GlimpseDbDriverDummy);
            var glimpseDbDriverActivator = new GlimpseDbDriverActivator();

            try
            {
                // Act
                glimpseDbDriverActivator.CreateDbDriver(dbDriverType);
                Assert.False(true, "Expected exception as not thrown");
            }
            catch (InvalidOperationException exception)
            {
                // Assert
                Assert.Equal(exception.Message, string.Format("{0} should have a parameterless constructor", dbDriverType));
            }
        }

        public class GlimpseDbDriverDummy
        {
            private readonly object _parameter;

            public GlimpseDbDriverDummy(object parameter)
            {
                _parameter = parameter;
            }

            public object Parameter
            {
                get { return _parameter; }
            }
        }
    }
}