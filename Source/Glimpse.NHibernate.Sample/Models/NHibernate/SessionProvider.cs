using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace Glimpse.NHibernate.Sample.Models.NHibernate
{
    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;

        public static void Initialize()
        {
            var cgf = new Configuration();
            var data = cgf.Configure(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Configuration\hibernate.cfg.xml"));
            cgf.AddDirectory(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Mappings")));
            
            _sessionFactory = data.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}