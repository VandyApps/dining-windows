using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SQLite2CEDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// MainViewModel constructor.
        /// Sets the ViewModel data objects to equal new instances of ObservableCollection.
        /// </summary>
        public MainViewModel()
        {
            this.countryData = new ObservableCollection<Country>();
            this.cityData = new ObservableCollection<City>();
            this.countryCapitals = new ObservableCollection<CountryCapitals>();
            this.countryPopulation = new ObservableCollection<CountryPopulationViewModel>();
        }

        /// <summary>
        /// A collection for Country objects.
        /// </summary>
        public ObservableCollection<Country> countryData { get; private set; }

        /// <summary>
        /// A collection for City objects.
        /// </summary>
        public ObservableCollection<City> cityData { get; private set; }

        /// <summary>
        /// A collection for CountryCapitals objects.
        /// </summary>
        public ObservableCollection<CountryCapitals> countryCapitals { get; private set; }

        /// <summary>
        /// A collection for CountryPopulationViewModel objects.
        /// </summary>
        public ObservableCollection<CountryPopulationViewModel> countryPopulation { get; private set; }

        /// <summary>
        /// A boolean value representing wheter or not ViewModel data has been loaded.
        /// </summary>
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Populates the ViewModel's data objects with items from the database.
        /// </summary>
        public void LoadData()
        {
            //Populates the countryData object with data in conjunction with getAllCountryData().
            foreach (Country thisCountry in getAllCountryData())
            {
                this.countryData.Add(thisCountry);
            }

            //Populates the cityData object with data in conjunction with getAllCityData().
            foreach (City thisCity in getAllCityData())
            {
                this.cityData.Add(thisCity);
            }

            /* Executes the GetResults method of CountryCapitals (within the myCountries context) and stores the result
             * of the LINQ statement in an IEnumerable. Populates the countryCapitals object with the IEnumerable results
             * of this method call. */
            IEnumerable<CountryCapitals> myCapitals = new CountryCapitals().GetResults(MainPage.myCountries);
            foreach (CountryCapitals thisCapital in myCapitals)
            {
                this.countryCapitals.Add(thisCapital);
            }

            /* Executes the GetResults method of CountryPopulation (within the myCountries context) and stores the result
             * of the LINQ expression in an IEnumerable. Populates the countryPopulation object with the IEnumerable results
             * of this method call and the addition of another LINQ statement that gets the country name from its ID. Items
             * are added to the countryPopulation object as new CountryPopulationViewModel objects which allows storage of
             * a string (country name) rather than a simple ID. */
            IEnumerable<CountryPopulation> myPopulation = new CountryPopulation().GetResults(MainPage.myCountries);
            foreach (CountryPopulation thisPop in myPopulation)
            {
                IEnumerable<string> cName = from eachCountry in MainPage.myCountries.Country
                                           where eachCountry.Id == thisPop.GroupingKeyCountryId
                                           select eachCountry.Name;
                this.countryPopulation.Add(new CountryPopulationViewModel() { countryName = cName.First().ToString(), countryPopulation = thisPop.Sum_ComputedColumn0.ToString() });
            }

            this.IsDataLoaded = true;
        }

        /// <summary>
        /// Executes a LINQ statement that returns all objects within MainPage.myCountries.City.
        /// </summary>
        /// <returns>An IEnumerable of type City containing all City objects within the database.</returns>
        private IEnumerable<City> getAllCityData()
        {
            return from eachCity in MainPage.myCountries.City
                   select eachCity;
        }

        /// <summary>
        /// Executes a LINQ statement that returns all objects within MainPage.myCountries.Country.
        /// </summary>
        /// <returns>An IEnumerable of type Country containing all Countries within the database.</returns>
        private IEnumerable<Country> getAllCountryData()
        {
            return from eachCountry in MainPage.myCountries.Country
                   select eachCountry;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}