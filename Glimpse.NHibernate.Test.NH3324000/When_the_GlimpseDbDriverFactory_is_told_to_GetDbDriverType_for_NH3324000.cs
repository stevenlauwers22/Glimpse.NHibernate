namespace Glimpse.NHibernate.Test.NH3324000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH3324000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            // 3324000 is a drop in replacement for 3314000
            return "3314000";
        }
    }
}