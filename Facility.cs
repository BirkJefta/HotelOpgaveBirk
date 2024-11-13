using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelOpgaveBirk {
    public class Facility {

        public int Facility_Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Facility_Id}, Name: {Name}";
        }
    }
}
