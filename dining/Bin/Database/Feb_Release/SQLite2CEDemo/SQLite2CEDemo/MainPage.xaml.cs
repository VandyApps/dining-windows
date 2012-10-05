using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;

namespace SQLite2CEDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string ConnectionString = @"isostore:/countries.sdf"; //Connection String to Database in Isolated Storage
        public static Countries myCountries; //Database reference
        public static City selectedCity; //Currently selected city (last city clicked on City pivot screen)

        /// <summary>
        /// MainPage Constructor.
        /// Initializes the connection to the database in isolated storage and copies a fresh copy of the database
        /// to isolated storage. If a copy of the database doesn't already exist in isolated storage then one
        /// is copied from content. Sets the Datacontext of the MainPage class to our MainViewModel through the
        /// App class. Sets an event handler for MainPage_Loaded.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            myCountries = new Countries(ConnectionString);
                    
            if (!myCountries.DatabaseExists())
                copyDatabaseToIsolatedStorage();

            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        /// <summary>
        /// Event triggered when MainPage is loaded. If data from the ViewModel hasn't been loaded yet then
        /// do so.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        /// <summary>
        /// Using the application's isolated storage, copy the countries.sdf database from content into isolated
        /// storage.
        /// </summary>
        private void copyDatabaseToIsolatedStorage()
        {
            using (IsolatedStorageFile str = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Stream database = Application.GetResourceStream(new Uri("countries.sdf", UriKind.Relative)).Stream;
                IsolatedStorageFileStream isoDatabase = new IsolatedStorageFileStream("countries.sdf", FileMode.Create, FileAccess.Write, str);
                database.CopyTo(isoDatabase);
                database.Close();
                isoDatabase.Close();
            }
        }

        /// <summary>
        /// Event triggered when the CityListBox is tapped. If an item within the listbox was also tapped then
        /// set selectedCity to equal a reference to that object and navigate to EditCityPage.xaml.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CityListBox_Tap(object sender, GestureEventArgs e)
        {
            if (CityListBox.SelectedItem != null)
            {
                selectedCity = CityListBox.SelectedItem as City;
                NavigationService.Navigate(new Uri("/EditCityPage.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Event triggered when the CountryListBox is tapped. If an item within the listbox was also tapped
        /// then create an object reference to the item/country selected and launch a web browser to the URL
        /// located within the Url property of that object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountryListBox_Tap(object sender, GestureEventArgs e)
        {
            if (CountryListBox.SelectedItem != null)
            {
                Country selectedCountry = CountryListBox.SelectedItem as Country;
                WebBrowserTask myTask = new WebBrowserTask();
                myTask.Uri = new Uri(selectedCountry.Url);
                myTask.Show();
            }
        }
    }
}