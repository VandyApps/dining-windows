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
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace dining
{

    [Table(Name = "dining")]
    public class Restaurant
    {
        private string _Name;
        [Column(Storage="_Name")]
        public string Name
        {
            get
            {
                return this._Name;
            }
        }
    }

    public class RestaurantList : List<Restaurant>
    {
        public RestaurantList()
        {

        }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

        }

        
    }
}