using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class Contract
    {
        public int ID { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndDate { get; set; }
        public int OrderNum { get; set; }
        public string Position { get; set; }
        //public int IndexOfContract { get; set; } // мб это в айди вставить?

        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public int ArtistCategoryId { get; set; }
        public virtual ArtistCategory ArtistCategory { get; set; }
        public int ContractTypeId { get; set; }
        public virtual ContractType ContractType { get; set; }
        // реализовать свзя (1 к 1 и т.д.) 
    }
}
