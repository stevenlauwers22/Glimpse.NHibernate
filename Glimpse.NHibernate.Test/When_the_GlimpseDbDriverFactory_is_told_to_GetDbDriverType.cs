using System.Linq;
using System.Reflection;
using Glimpse.NHibernate.AlternateType;
using Glimpse.NHibernate.Inspector.Core.NHibernateDbDriverWrapper;
using Xunit;

namespace Glimpse.NHibernate.Test
{
    public abstract class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        [Fact]
        public void It_should_return_the_db_driver_type()
        {
            // Arrange
            var nhibernateAssembly = Assembly.Load("NHibernate");
            var glimpseDbDriverFactory = new GlimpseDbDriverFactory();

            // Act
            var result = glimpseDbDriverFactory.GetDbDriverType(nhibernateAssembly);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.GetInterfaces().ToList().Contains(typeof(IGlimpseDbDriver)));

            var versionNumber = GetExpectedVersionNumber();
            var driver = string.Format("Glimpse.NHibernate.AlternateType.GlimpseDbDriverNh{0}", versionNumber);
            Assert.Equal(driver, result.FullName);
        }

        protected abstract string GetExpectedVersionNumber();
    }
}