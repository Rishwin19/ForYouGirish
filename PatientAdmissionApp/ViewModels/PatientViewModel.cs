using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PatientAdmissionApp
{
    public class PatientViewModel : BaseViewModel, IPatient
    {
        private MainWindow mainWindow;

        public event EventHandler AppointmentUpdated;
        public event EventHandler PatientRegistered;
        public event EventHandler<PatientModel> PatientUpdated;

        public ObservableCollection<PatientModel> Patients { get; set; } = new ObservableCollection<PatientModel>();
        public ObservableCollection<PatientModel> ConfirmedPatients { get; set; } = new ObservableCollection<PatientModel>();

        private PatientModel _newPatient;
        public PatientModel NewPatient
        {
            get { return _newPatient; }
            set { _newPatient = value; OnPropertyChanged(); }
        }

        private PatientModel _selectedPatient;
        public PatientModel SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value; OnPropertyChanged(); }
        }

        private bool _selectedSlot;
        public bool SelectedSlot
        {
            get { return _selectedSlot; }
            set { _selectedSlot = value; OnPropertyChanged(nameof(SelectedSlot)); }
        }
       
        public ICommand RegisterPatientCommand { get; set; }
        public ICommand SendUpdateCommand { get; set; }

        public PatientViewModel()
        {
            NewPatient = new PatientModel();
            RegisterPatientCommand = new RelayCommand(RegisterPatient);
            SendUpdateCommand = new RelayCommand(SendUpdate);
            PatientRegistered += OnPatientRegistered;
            AppointmentUpdated += OnAppointmentUpdated;

        }

        public void RegisterPatient(object parameter)
        {
            PatientRegistered?.Invoke(this, EventArgs.Empty);
        }

        private void OnPatientRegistered(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NewPatient.Name) && NewPatient.Dateofbirth != default)
            {
                Patients.Add(new PatientModel
                {
                    Name = NewPatient.Name,
                    Dateofbirth = NewPatient.Dateofbirth,
                    Age = DateTime.Now.Year - NewPatient.Dateofbirth.Year,
                    Address = NewPatient.Address,
                    Slot = NewPatient.Slot,
                    BookingDate = NewPatient.BookingDate
                });
                PatientUpdated?.Invoke(this, NewPatient);
                NewPatient = new PatientModel();
            }
            else
            {
                MessageBox.Show("Please provide valid patient details.");
            }
        }


        public void SendUpdate(object parameter)
        {
            if (SelectedPatient != null)
            {
                SelectedPatient.ConfirmationStatus = NewPatient.ConfirmationStatus;
                SelectedPatient.AppointmentDate = NewPatient.AppointmentDate;
                AppointmentUpdated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Please select a Patient");
            }
        }

        private void OnAppointmentUpdated(object sender, EventArgs e)
        {
            if (SelectedPatient != null && !ConfirmedPatients.Contains(SelectedPatient))
            {
                ConfirmedPatients.Add(SelectedPatient);
                MessageBox.Show($"Appointment Confirmed for {SelectedPatient.Name}");
            }
        }
    }
}
