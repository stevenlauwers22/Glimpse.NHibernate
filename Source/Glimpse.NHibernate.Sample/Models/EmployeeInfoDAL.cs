using System.Collections.Generic;
using Glimpse.NHibernate.Sample.Models.NHibernate;

namespace Glimpse.NHibernate.Sample.Models
{
    public class EmployeeInfoDAL
    {
        public IList<EmployeeInfo> GetEmployees()
        {
            using (var session = SessionProvider.OpenSession())
            {
                var query = session.QueryOver<EmployeeInfo>();
                return query.List<EmployeeInfo>();
            }
        }

        public EmployeeInfo GetEmployeeById(int id)
        {
            using (var session = SessionProvider.OpenSession())
            {
                return session.Get<EmployeeInfo>(id);
            }
        }

        public void CreateEmployee(EmployeeInfo employeeInfo)
        {
            using (var session = SessionProvider.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Save(employeeInfo);
                    tran.Commit();
                }
            }
        }

        public void UpdateEmployee(EmployeeInfo employeeInfo)
        {
            using (var session = SessionProvider.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Update(employeeInfo);
                    tran.Commit();
                }
            }
        }

        public void DeleteEmployee(EmployeeInfo employeeInfo)
        {
            using (var session = SessionProvider.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Delete(employeeInfo);
                    tran.Commit();
                }
            }
        }
    }
}