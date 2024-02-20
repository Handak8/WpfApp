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
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EditAbonentWindow.xaml
    /// </summary>
    public partial class EditAbonentWindow : Window
    {
        public EditAbonentWindow(Abonent abonentToEdit)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MainViewModel.SelectedAbonent = abonentToEdit;
            MainViewModel.Country = abonentToEdit.Country;
            MainViewModel.City = abonentToEdit.City;
            MainViewModel.Pnumber = abonentToEdit.Pnumber;
            MainViewModel.Fax = abonentToEdit.Fax;
            MainViewModel.Description = abonentToEdit.Description;
            MainViewModel.Secure = abonentToEdit.Secure;
        }
    }
}
