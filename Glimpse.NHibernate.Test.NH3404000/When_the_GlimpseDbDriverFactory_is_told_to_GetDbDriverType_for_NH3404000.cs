namespace Glimpse.NHibernate.Test.NH3404000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH3404000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "3400";
        }
    }
}