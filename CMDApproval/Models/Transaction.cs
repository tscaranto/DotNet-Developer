using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDApproval.Models
{
    public class Transaction
    {
        [Key]
        public Int32 Id { get; set; }

        public String Type { get; set; }

        public StatusEnum Status { get; set; }

        public Int32 AccountTo { get; set; }

        public Int32 AccountFrom { get; set; }

        public Decimal Amount { get; set; }

        public DateTime DtInserted { get; set; }
    }

    public enum StatusEnum
    {
        Aproved = 1,
        Pending = 2
    }
}
