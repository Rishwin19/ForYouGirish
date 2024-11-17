using System.Windows;

namespace PatientAdmissionApp
{
    public partial class MainWindow : Window
    {
        private PatientViewModel _viewModel;
      

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new PatientViewModel(this);
            DataContext = _viewModel;
        }
    }
}
