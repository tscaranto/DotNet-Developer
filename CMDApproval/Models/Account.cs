using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDApproval.Models
{
    public class Account
    {
        [Key]
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Decimal Balance { get; set; }

    }
}
