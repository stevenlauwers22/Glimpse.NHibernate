using NHibernate.Cfg;

namespace Glimpse.NHibernate.Test.NH4014000
{
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_told_to_Execute_for_NH4014000
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