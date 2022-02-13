using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserDBContext:DbContext
    {
        //abstract method.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //this method decides to what database our project is based on.
        {
            //  @ helps visual studio to understand "\" as "\", otherwise we should use "\\" instead of "\". Because "\" has a syntatic meaning in C#
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=UserDB;Trusted_Connection=true");


        }

        //we've declared which database are we going to use above, 
        //now it's time to declare which class object is equal to which db object. 
        public DbSet<User> Users { get; set; }
        

    }
}
