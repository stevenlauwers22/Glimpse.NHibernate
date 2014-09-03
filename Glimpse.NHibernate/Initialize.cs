using Glimpse.NHibernate.Inspector.Core;

namespace Glimpse.NHibernate
{
    public static class Initialize
    {
        public static void Start()
        {
            NHibernateExecutionBlock.Instance.Execute();
        }
    }
}