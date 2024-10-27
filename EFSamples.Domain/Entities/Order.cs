using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSamples.Domain.Entities
{
    public class Order:BaseEntity
    {
        public string Code { get; set; } = null!;
        public long UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFinaly { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
