namespace Glimpse.NHibernate.Test.NH4024000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4024000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 4024000 is a drop in replacement for 4004000
            return "4004000";
        }
    }
}