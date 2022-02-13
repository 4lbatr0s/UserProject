using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, UserDBContext>, IUserDal
    {
        public List<User> GetAllTest() //this function created in order to escape Moq's rule of no optional parameter use.
        {   using (var context = new UserDBContext()) //we basically override the GetAll() method in the Core/DataAccess/IEntityRepository.cs file.
            {
                var result = from user in context.Users
                             select user;

                return result.ToList(); 
            }
        }

        public List<User> GetByDateTest(string date)
        {
            using (var context = new UserDBContext()) //we basically override the GetAll() method in the Core/DataAccess/IEntityRepository.cs file.
            {
                var result = from user in context.Users
                             where user.CreatedAt.Equals(Guid.Parse(date))
                             select user;
                return result.ToList();
            }
        }
    }
}
