namespace Glimpse.NHibernate.Test.NH4044000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4044000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 4044000 is a drop in replacement for 4004000
            return "4004000";
        }
    }
}