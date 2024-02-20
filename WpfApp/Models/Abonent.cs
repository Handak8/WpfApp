using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Abonent
    {
        public int Id { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public int Pnumber { get; set; }
        public int? Fax { get; set; }
        public string? Description { get; set; }
        public int Ptype { get; set; }
        public int? Secure { get; set; }
        [NotMapped]
        public AbonentType AbType 
        {
            get
            {
                return DataAccess.GetAbonentTypeById(Ptype);
            }
        }
    }
}
