using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PatientAdmissionApp
{
    public class PatientViewModel : BaseViewModel, IPatient
    {
        private MainWindow _mainWindow;

        public event EventHandler AppointmentUpdated;
        public event EventHandler PatientRegistered;
        public event EventHandler<PatientModel> PatientUpdated;

        public ICommand ShowRegistrationCommand { get; set; }
        public ICommand ShowAppointmentCommand { get; set; }
        public ICommand ShowDashboardCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        private PatientRegistrationControl registrationControl;
        private AppointmentControl appointmentControl;
        private PatientDashboardControl dashboardControl;

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

        public PatientViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            NewPatient = new PatientModel();
            ShowRegistrationCommand = new RelayCommand(ShowRegistration);
            ShowAppointmentCommand = new RelayCommand(ShowAppointment);
            ShowDashboardCommand = new RelayCommand(ShowDashboard);
            ExitCommand = new RelayCommand(Exit);

            // Initialize controls (if needed)
            registrationControl = new PatientRegistrationControl();
            appointmentControl = new AppointmentControl();
            dashboardControl = new PatientDashboardControl();

            RegisterPatientCommand = new RelayCommand(RegisterPatient);
            SendUpdateCommand = new RelayCommand(SendUpdate);

            PatientUpdated += registrationControl.DisplayPatientName;
            PatientUpdated += appointmentControl.DisplayPatientName;
            PatientUpdated += dashboardControl.DisplayPatientName;

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

        // Command method implementations
        private void ShowRegistration(object parameter)
        {
            // Logic for showing the Registration control
            // Switch MainContent to Registration control
            _mainWindow.MainContent.Content = registrationControl;
        }

        private void ShowAppointment(object parameter)
        {
            // Logic for showing the Appointment control
            // Switch MainContent to Appointment control
            _mainWindow.MainContent.Content = appointmentControl;
        }

        private void ShowDashboard(object parameter)
        {
            // Logic for showing the Dashboard control
            // Switch MainContent to Dashboard control
            _mainWindow.MainContent.Content = dashboardControl;
        }

        private void Exit(object parameter)
        {
            // Logic to handle exit (close window, or some other action)
            Application.Current.Shutdown();
        }

    }
}
