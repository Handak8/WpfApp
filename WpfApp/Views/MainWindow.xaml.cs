using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllAbonentTypesView;
        public static ListView AllAbonentsView;
        public static ListView AllAbonentServicesView;
        public static ListView AllAbonentDetailsView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            AllAbonentTypesView = ViewAllAbonentTypes;
            AllAbonentsView = ViewAllAbonents;
            AllAbonentDetailsView = ViewAllAbonentDetails;
            AllAbonentServicesView = ViewAllAbonentServices;

        }
    }
}