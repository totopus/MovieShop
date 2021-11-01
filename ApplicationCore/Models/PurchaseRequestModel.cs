using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public PurchaseRequestModel()
        {
            PurchaseDateTime = DateTime.Now;
            PurchaseNumber = Guid.NewGuid();
        }

        public Guid PurchaseNumber { get; }
        public DateTime PurchaseDateTime { get; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
    }
}

