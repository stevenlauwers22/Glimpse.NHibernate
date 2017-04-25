namespace Glimpse.NHibernate.Test.NH4034000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4034000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 4034000 is a drop in replacement for 4004000
            return "4004000";
        }
    }
}