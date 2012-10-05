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

namespace SQLite2CEDemo
{
    public partial class EditCityPage : PhoneApplicationPage
    {
        /// <summary>
        /// EditCityPage constructor.
        /// Sets the DataContext of the page to equal selectedCity.
        /// </summary>
        public EditCityPage()
        {
            InitializeComponent();

            DataContext = MainPage.selectedCity;
        }

        /// <summary>
        /// Event triggered when navigating away from the page. The event saves any changes made to the database
        /// by the user through altering TextBox values. Data for all objects witin the ViewModel are cleared and
        /// re-loaded with the new values.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MainPage.myCountries.SubmitChanges();
            App.ViewModel.cityData.Clear();
            App.ViewModel.countryData.Clear();
            App.ViewModel.countryCapitals.Clear();
            App.ViewModel.countryPopulation.Clear();
            App.ViewModel.LoadData();
        }
        
        /// <summary>
        /// Event triggered when the btnDone button is clicked. Event triggers a command equivalent to the user
        /// pressing the back button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}