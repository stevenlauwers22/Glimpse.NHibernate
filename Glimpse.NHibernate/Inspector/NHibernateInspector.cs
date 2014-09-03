using Glimpse.Core.Extensibility;
using Glimpse.NHibernate.Inspector.Core;

namespace Glimpse.NHibernate.Inspector
{
    public class NHibernateInspector 
        : IInspector
    {
        public void Setup(IInspectorContext context)
        {
            NHibernateExecutionBlock.Instance.Execute();
        }
    }
}
