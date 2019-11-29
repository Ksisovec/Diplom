using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class ConcertEvent
    {
        public int ID { get; set; }
        public DateTime? BeginningDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public int ConcertPlaceTypeId { get; set; }
        public virtual ConcertPlaceType ConcertPlaceType { get; set; }
        public int ConcertTypeId { get; set; }
        public virtual ConcertType ConcertType { get; set; }

        public virtual ICollection<ConcertMarks> ConcertMarks { get; set; }
    }
}
