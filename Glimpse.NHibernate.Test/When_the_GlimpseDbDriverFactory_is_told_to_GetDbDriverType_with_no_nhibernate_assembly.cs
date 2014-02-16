using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test
{
    [TestFixture]
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_with_no_nhibernate_assembly
    {
        [Test]
        public void It_should_return_null()
        {
            // Arrange
            var glimpseDbDriverFactory = new GlimpseDbDriverFactory();

            // Act
            var result = glimpseDbDriverFactory.GetDbDriverType(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}