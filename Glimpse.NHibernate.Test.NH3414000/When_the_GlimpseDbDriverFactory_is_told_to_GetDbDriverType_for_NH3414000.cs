namespace Glimpse.NHibernate.Test.NH3414000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH3414000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "3400";
        }
    }
}