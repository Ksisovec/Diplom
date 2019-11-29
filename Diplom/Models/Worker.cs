using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class Worker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string RegistrationPlace { get; set; }
        public string BirthPlace { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public bool Sex { get; set; }
        public bool MaritalStatus { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        //public string PlaceOfBrith { get; set; }
        public int DepartamentID { get; set; }
        public virtual Departament Departament { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual ICollection<ConcertMarks> ConcertMarks { get; set; }

        public virtual ICollection<LessonMarks> LessonMarks { get; set; }

        public User User { get; set; }
    }
}
