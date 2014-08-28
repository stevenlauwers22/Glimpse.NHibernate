using System;
using Glimpse.NHibernate.AlternateType;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public class When_the_GlimpseDbDriverActivator_is_told_to_CreateDbDriver
    {
        [Test]
        public void It_should_return_the_db_driver()
        {
            // Arrange
            var dbDriverType = typeof(GlimpseDbDriverDummy);
            var glimpseDbDriverActivator = new GlimpseDbDriverActivator();

            // Act
            var result = glimpseDbDriverActivator.CreateDbDriver(dbDriverType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<GlimpseDbDriverDummy>(result);
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