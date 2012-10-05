using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SQLite2CEDemo
{
    public class CountryPopulationViewModel : INotifyPropertyChanged
    {
        private string _countryName;

        public string countryName
        {
            get
            {
                return _countryName;
            }
            set
            {
                if (value != _countryName)
                {
                    _countryName = value;
                    NotifyPropertyChanged("countryName");
                }
            }
        }

        private string _countryPopulation;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string countryPopulation
        {
            get
            {
                return _countryPopulation;
            }
            set
            {
                if (value != _countryPopulation)
                {
                    _countryPopulation = value;
                    NotifyPropertyChanged("countryPopulation");
                }
            }
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