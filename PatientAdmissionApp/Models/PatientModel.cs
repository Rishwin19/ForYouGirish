using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdmissionApp
{
    public class PatientModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Dateofbirth { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public string BookingDate { get; set; }
        public string AppointmentDate { get; set; } 
        public string ConfirmationStatus { get; set; } = "Pending";
        public string Slot { get; set; }
    }
}
