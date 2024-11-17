using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatientAdmissionApp
{
    /// <summary>
    /// Interaction logic for PatientRegistrationControl.xaml
    /// </summary>
    public partial class PatientRegistrationControl : UserControl
    {
        public PatientRegistrationControl()
        {
            InitializeComponent();
        }
        public void DisplayPatientName(object sender, PatientModel e)
        {
            if (e != null)
            {
                PatientNameLabel.Content = $"Notification from Registration page - {e.Name}";
            }
        }


    }
}
