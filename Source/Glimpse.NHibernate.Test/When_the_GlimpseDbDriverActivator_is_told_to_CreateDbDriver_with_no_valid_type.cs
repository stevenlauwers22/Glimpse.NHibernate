using System;
using Glimpse.NHibernate.AlternateType;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_GlimpseDbDriverActivator_is_told_to_CreateDbDriver_with_no_valid_type
    {
        [Fact]
        public void It_should_throw_an_invalid_cast_exception()
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
            catch (InvalidCastException exception)
            {
                // Assert
                Assert.Equal(string.Format("Unable to cast object of type '{0}' to type '{1}'.", dbDriverType.Name, typeof(IGlimpseDbDriver).FullName), exception.Message);
            }
        }

        public class GlimpseDbDriverDummy
        {
        }
    }
}