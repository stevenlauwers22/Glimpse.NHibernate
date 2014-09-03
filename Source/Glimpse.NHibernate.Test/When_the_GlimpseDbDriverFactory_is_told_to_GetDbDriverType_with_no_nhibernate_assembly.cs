using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_with_no_nhibernate_assembly
    {
        [Fact]
        public void It_should_return_null()
        {
            // Arrange
            var glimpseDbDriverFactory = new GlimpseDbDriverFactory();

            // Act
            var result = glimpseDbDriverFactory.GetDbDriverType(null);

            // Assert
            Assert.Null(result);
        }
    }
}