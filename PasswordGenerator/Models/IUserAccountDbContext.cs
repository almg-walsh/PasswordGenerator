using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Models
{
    public interface IUserAccountDbContext 
    {
        DbSet<UserAccount> UserAccount { get; set; }
        void Commit();
    }
}
