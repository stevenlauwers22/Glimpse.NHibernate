namespace Glimpse.NHibernate.Test.NH1214000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH1214000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "1214000";
        }
    }
}