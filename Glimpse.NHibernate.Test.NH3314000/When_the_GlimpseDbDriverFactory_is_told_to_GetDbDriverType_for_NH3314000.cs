﻿namespace Glimpse.NHibernate.Test.NH3314000
{
    public class When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType_for_NH3314000
        : When_the_GlimpseDbDriverFactory_is_told_to_GetDbDriverType
    {
        protected override string GetExpectedVersionNumber()
        {
            return "3314000";
        }
    }
}