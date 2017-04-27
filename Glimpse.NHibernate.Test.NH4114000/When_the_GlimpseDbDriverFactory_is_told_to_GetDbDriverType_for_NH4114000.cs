namespace Glimpse.NHibernate.Test.NH4114000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4114000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "4104000";
        }
    }
}