using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDApproval
{
    public class SuperbidContext : DbContext
    {
        public SuperbidContext() : base("name=SuperbidConn")
        {

        }

        public System.Data.Entity.DbSet<Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<Models.Transaction> Transactions { get; set; }
    }
}
