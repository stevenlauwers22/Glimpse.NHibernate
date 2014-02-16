using System;
using Glimpse.NHibernate.AlternateType;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public class When_the_GlimpseDbDriverActivator_is_told_to_CreateDbDriver_with_no_valid_type
    {
        [Test]
        public void It_should_throw_an_invalid_cast_exception()
        {            
            // Arrange
            var dbDriverType = typeof(GlimpseDbDriverDummy);
            var glimpseDbDriverActivator = new GlimpseDbDriverActivator();

            try
            {
                // Act
                glimpseDbDriverActivator.CreateDbDriver(dbDriverType);
            }
            catch (InvalidCastException exception)
            {
                // Assert
                Assert.AreEqual(string.Format("Unable to cast object of type '{0}' to type '{1}'.", dbDriverType.Name, typeof(IGlimpseDbDriver).FullName), exception.Message);
                Assert.Pass();
            }

            Assert.Fail("Expected exception as not thrown");
        }

        public class GlimpseDbDriverDummy
        {
        }
    }
}