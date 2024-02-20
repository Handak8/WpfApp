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
    /// Логика взаимодействия для EditAbonentTypeWindow.xaml
    /// </summary>
    public partial class EditAbonentTypeWindow : Window
    {
        public EditAbonentTypeWindow(AbonentType abonentTypeToEdit)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MainViewModel.SelectedAbonentType = abonentTypeToEdit;
            MainViewModel.AbonentTypeName = abonentTypeToEdit.Name;
            MainViewModel.Mobile = abonentTypeToEdit.Mobile;
        }
    }
}
