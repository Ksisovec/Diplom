using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class LessonEvent
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<LessonMarks> LessonMarks { get; set; }
    }
}
