namespace Glimpse.NHibernate.Test.NH4014000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4014000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 4014000 is a drop in replacement for 4004000
            return "4004000";
        }
    }
}