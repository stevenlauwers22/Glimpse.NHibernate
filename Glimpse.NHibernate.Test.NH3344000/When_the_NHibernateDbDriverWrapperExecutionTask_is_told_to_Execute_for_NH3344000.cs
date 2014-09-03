using NHibernate.Cfg;

namespace Glimpse.NHibernate.Test.NH3344000
{
    public class When_the_NHibernateDbDriverWrapperExecutionTask_is_told_to_Execute_for_NH3344000
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