﻿using NHibernate.Cfg;
using NUnit.Framework;

namespace Glimpse.NHibernate.Test.NH4004000
{
    [TestFixture]
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_told_to_Execute_for_NH4004000
        : When_the_NHibernateDbDriverWrapperExecutionTask_is_told_to_Execute
    {
        protected override void BuildSessionFactory()
        {
            new Configuration()
                .Configure()
                .BuildSessionFactory();
        }
    }
}