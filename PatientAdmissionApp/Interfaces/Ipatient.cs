using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAdmissionApp
{
    public interface IPatient
    {
        ObservableCollection<PatientModel> Patients { get; }
       void RegisterPatient(object parameter);
       void SendUpdate(object parameter);
    }
}
