using NUnit.Framework;

namespace Glimpse.NHibernate.Test.NH3334000
{
    [TestFixture]
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH3334000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 3334000 is a drop in replacement for 3314000
            return "3314000";
        }
    }
}