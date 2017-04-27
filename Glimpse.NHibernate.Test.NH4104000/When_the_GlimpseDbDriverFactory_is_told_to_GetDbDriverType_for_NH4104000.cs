namespace Glimpse.NHibernate.Test.NH4104000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH4104000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "4104000";
        }
    }
}