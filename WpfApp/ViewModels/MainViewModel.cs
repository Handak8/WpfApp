using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using System.ComponentModel;
using WpfApp.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private List<AbonentType> allAbonentTypes = DataAccess.GetAllAbonentTypes();
        public List<AbonentType> AllAbonentTypes
        {
            get { return allAbonentTypes; }
            set 
            { 
                allAbonentTypes = value;
                NotifyPropertyChanged("AllAbonentTypes");
            }
        }

        private List<Abonent> allAbonents = DataAccess.GetAllAbonents();
        public List<Abonent> AllAbonents
        { 
            get {  return allAbonents; }
            set 
            { 
                allAbonents = value;
                NotifyPropertyChanged("AllAbonents");
            }
        }

        private List<AbonentServices> allAbonentServices = DataAccess.GetAbonentServices();
        public List<AbonentServices> AllAbonentServices
        {
            get { return allAbonentServices; }
            set
            {
                allAbonentServices = value;
                NotifyPropertyChanged("AllAbonentServices");
            }
        }

        private List<AbonentDetails> allAbonentDetails = DataAccess.GetAbonentDetails();
        public List<AbonentDetails> AllAbonentDetails
        {
            get { return allAbonentDetails; }
            set
            {
                allAbonentDetails = value;
                NotifyPropertyChanged("AllAbonentDetails");
            }
        }
       
        //свойства для типа абонента
        public static string AbonentTypeName { get; set; }
        public static Boolean Mobile { get; set; }

        //Свойства для абонента
        public static int Country {  get; set; }
        public static int City { get; set; }
        public static int Pnumber {  get; set; }
        public static int? Fax { get; set; }
        public static string? Description { get; set; }
        public static int? Secure {  get; set; }
        public static AbonentType Ptype { get; set; }

        //Свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Abonent SelectedAbonent { get; set; }
        public static AbonentType SelectedAbonentType { get; set; }

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get 
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    if(SelectedTabItem.Name == "AbonetnTab" && SelectedAbonent != null) 
                    {
                        DataAccess.DeleteAbonent(SelectedAbonent);
                        UpdateAll();
                        
                    }
                    else if(SelectedTabItem.Name == "AbonentTypeTab" && SelectedAbonentType != null) 
                    {
                        DataAccess.DeleteAbonentType(SelectedAbonentType);
                        UpdateAll();
                    }
                });
            }
        }
        #region COMMANDS TO OPEN WINDOW
        private RelayCommand openAddNewAbonentTypeWind;
        public RelayCommand OpenAddNewAbonentTypeWind
        {
            get 
            {
                return openAddNewAbonentTypeWind ?? new RelayCommand(obj =>
                {
                    OpenAddNewAbonentTypeWindow();
                });
            }
        }
        private RelayCommand openAddNewAbonentWind;
        public RelayCommand OpenAddNewAbonentWind
        {
            get
            {
                return openAddNewAbonentWind ?? new RelayCommand(obj =>
                {
                    OpenAddNewAbonentWindow();
                });
            }
        }

        public RelayCommand openEditAbonentTypeWind;
        public RelayCommand OpenEditAbonentTypeWind
        {
            get
            {
                return openEditAbonentTypeWind ?? new RelayCommand(obj =>
                {
                    OpenEditAbonentTypeWindow(SelectedAbonentType);
                });
            }
        }

        public RelayCommand openEditAbonentWind;
        public RelayCommand OpenEditAbonentWind
        {
            get
            {
                return openEditAbonentWind ?? new RelayCommand(obj =>
                {
                    OpenEditAbonentWindow(SelectedAbonent);
                });
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOW
        private void SetCenterPositionAndOpen(Window window) 
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void OpenAddNewAbonentTypeWindow()
        {
            AddNewAbonentTypeWindow addNewAbonentTypeWindow = new AddNewAbonentTypeWindow();
            SetCenterPositionAndOpen(addNewAbonentTypeWindow);
        }

        private void OpenAddNewAbonentWindow()
        {
            AddNewAbonentWindow addNewAbonentWindow = new AddNewAbonentWindow();
            SetCenterPositionAndOpen(addNewAbonentWindow);
        }
        private void OpenEditAbonentTypeWindow(AbonentType abonentType)
        {
            EditAbonentTypeWindow editAbonentTypeWindow = new EditAbonentTypeWindow(abonentType);
            SetCenterPositionAndOpen(editAbonentTypeWindow);
        }

        private void OpenEditAbonentWindow(Abonent abonent)
        {
            EditAbonentWindow editAbonentWindow = new EditAbonentWindow(abonent);
            SetCenterPositionAndOpen(editAbonentWindow);
        }
        #endregion

        #region UPDATE VIEWS
        private void UpdateAllAbonentTypesView() 
        {
            AllAbonentTypes = DataAccess.GetAllAbonentTypes();
            MainWindow.AllAbonentTypesView.ItemsSource = null;
            MainWindow.AllAbonentTypesView.Items.Clear();
            MainWindow.AllAbonentTypesView.ItemsSource = AllAbonentTypes;
            MainWindow.AllAbonentTypesView.Items.Refresh();

        }

        private void UpdateAllAbonentsView()
        {
            AllAbonents = DataAccess.GetAllAbonents();
            MainWindow.AllAbonentsView.ItemsSource = null;
            MainWindow.AllAbonentsView.Items.Clear();
            MainWindow.AllAbonentsView.ItemsSource = AllAbonents;
            MainWindow.AllAbonentsView.Items.Refresh();

        }

        private void UpdateAllAbonentServicesViiew()
        {
            AllAbonentServices = DataAccess.GetAbonentServices();
            MainWindow.AllAbonentServicesView.ItemsSource = null;
            MainWindow.AllAbonentServicesView.Items.Clear();
            MainWindow.AllAbonentServicesView.ItemsSource = AllAbonentServices;
            MainWindow.AllAbonentServicesView.Items.Refresh();
        }

        private void UpdateAllAbonentDetailsViiew()
        {
            AllAbonentDetails = DataAccess.GetAbonentDetails();
            MainWindow.AllAbonentDetailsView.ItemsSource = null;
            MainWindow.AllAbonentDetailsView.Items.Clear();
            MainWindow.AllAbonentDetailsView.ItemsSource = AllAbonentDetails;
            MainWindow.AllAbonentDetailsView.Items.Refresh();
        }

        private void UpdateAll() 
        {
            UpdateAllAbonentsView();
            UpdateAllAbonentTypesView();
            UpdateAllAbonentServicesViiew();
            UpdateAllAbonentDetailsViiew();
        }
        #endregion

        #region COMMANDS TO ADD
        private RelayCommand addNewAbonentType;
        public RelayCommand AddNewAbonentType
        {
            get
            {
                return addNewAbonentType ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if(AbonentTypeName == null)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else 
                    {
                        DataAccess.CreateAbonentType(AbonentTypeName, Mobile);
                        UpdateAllAbonentTypesView();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand addNewAbonent;
        public RelayCommand AddNewAbonent
        {
            get
            {
                return addNewAbonent ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    if(Country == null)
                    {
                        SetRedBlockControll(wnd, "Country");
                    }
                    else if(City == null)
                    {
                        SetRedBlockControll(wnd, "City");
                    }
                    else if (Pnumber == null)
                    {
                        SetRedBlockControll(wnd, "Number");
                    }
                    else if(Ptype == null) 
                    {
                        SetRedBlockControll(wnd, "Ptype");
                    }
                    else
                    {
                        DataAccess.CreateAbonent(Country, City, Pnumber, Fax, Description, Ptype.Id, Secure);
                        UpdateAllAbonentsView();
                        wnd.Close();
                    }
                   
                });
            }
        }


        #endregion

        #region COMMANDS TO EDIT
        private RelayCommand editAbonentType;
        public RelayCommand EditAbonentType
        {
            get
            {
                return editAbonentType ?? new RelayCommand(obj => 
                {
                    Window window = obj as Window;
                    if(SelectedAbonentType != null) 
                    {
                        if (AbonentTypeName == null)
                        {
                            SetRedBlockControll(window, "NameBlock");
                        }
                        else
                        {
                            DataAccess.EditAbonentType(SelectedAbonentType, AbonentTypeName, Mobile);
                            UpdateAllAbonentTypesView();
                            window.Close();
                        }
                    }
                });
            }
        }

        private RelayCommand editAbonent;
        public RelayCommand EditAbonent
        {
            get
            {
                return editAbonent ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedAbonent != null) 
                    {
                        if (Country == null)
                        {
                            SetRedBlockControll(window, "Country");
                        }
                        else if (City == null)
                        {
                            SetRedBlockControll(window, "City");
                        }
                        else if (Pnumber == null)
                        {
                            SetRedBlockControll(window, "Number");
                        }
                        else if (Ptype == null)
                        {
                            //SetRedBlockControll(window, "Ptype");
                        }
                        else
                        {
                            DataAccess.EditAbonent(SelectedAbonent, Country, City, Pnumber, Fax, Description, Ptype.Id, Secure);
                            UpdateAllAbonentsView();
                            window.Close();
                        }
                    }
                });
            }
        }
        #endregion

        #region COMMANDS TO GENERATE REPORTS
        private RelayCommand generateReportOne;
        public RelayCommand GenerateReportOne
        {
            get
            {
                return generateReportOne ?? new RelayCommand(obj =>
                {
                    DataAccess.GenerateReport1();
                });
            }
        }

        private RelayCommand generateReportTwo;
        public RelayCommand GenerateReportTwo
        {
            get
            {
                return generateReportTwo ?? new RelayCommand(obj =>
                {
                    DataAccess.GenerateReport2();
                });
            }
        }
        private RelayCommand generateReportThree;
        public RelayCommand GenerateReportThree
        {
            get
            {
                return generateReportThree ?? new RelayCommand(obj =>
                {
                    DataAccess.GenerateReport3();
                });
            }
        }
        #endregion
        private void SetRedBlockControll(Window wnd, string blockName) 
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) 
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
