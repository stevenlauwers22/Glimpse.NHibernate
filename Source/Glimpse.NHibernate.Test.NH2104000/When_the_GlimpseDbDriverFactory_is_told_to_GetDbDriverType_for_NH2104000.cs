using NUnit.Framework;

namespace Glimpse.NHibernate.Test.NH2104000
{
    [TestFixture]
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH2104000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "2104000";
        }
    }
}