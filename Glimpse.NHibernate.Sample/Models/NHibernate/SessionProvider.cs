using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Cfg;

namespace Glimpse.NHibernate.Sample.Models.NHibernate
{
    public class SessionProvider
    {
        private static ISessionFactory _sessionFactory;

        public static void Initialize()
        {
            var configuration = new Configuration()
                .Configure(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Configuration\hibernate.cfg.xml"))
                .AddDirectory(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(@"\Models\NHibernate\Mappings")))
                .AddProperties(new Dictionary<string, string>
                {
                    {Environment.BatchStrategy, typeof(NonBatchingBatcherFactory).FullName}
                })
                ;
            
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}