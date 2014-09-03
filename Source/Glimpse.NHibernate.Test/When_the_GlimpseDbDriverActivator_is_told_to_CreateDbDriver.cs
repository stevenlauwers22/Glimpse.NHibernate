using System;
using Glimpse.NHibernate.AlternateType;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_GlimpseDbDriverActivator_is_told_to_CreateDbDriver
    {
        [Fact]
        public void It_should_return_the_db_driver()
        {
            // Arrange
            var dbDriverType = typeof(GlimpseDbDriverDummy);
            var glimpseDbDriverActivator = new GlimpseDbDriverActivator();

            // Act
            var result = glimpseDbDriverActivator.CreateDbDriver(dbDriverType);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<GlimpseDbDriverDummy>(result);
        }

        public class GlimpseDbDriverDummy : IGlimpseDbDriver
        {
            public void Wrap(object driver)
            {
                throw new NotImplementedException();
            }
        }
    }
}