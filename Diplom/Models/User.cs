using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class User
    {
        [Key]
        [ForeignKey("Worker")]
        public int ID { get; set; }
        public string Password { get; set; }
        public Worker Worker { get; set; }
    }
}
