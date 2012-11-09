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
using Microsoft.Phone.Data.Linq;
using Microsoft.Phone.Data.Linq.Mapping;
using System.IO;
using System.IO.IsolatedStorage;


namespace Windows_Dining
{
    public partial class DiningDetail : PhoneApplicationPage
    {
        public DiningDetail()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DBHelper.MoveReferenceDatabase();
            IList<Dining1> restaurants = DBHelper.GetRestaurants();

            string name;

            if (NavigationContext.QueryString.TryGetValue("name", out name))
            {
                //Decreases fontsize if the name is too long
                if (name.Length > 12)
                {
                    PageTitle.FontSize -= 10;
                }

                PageTitle.Text = name;

                for (int i = 0; i < restaurants.Count(); i++)
                {
                    if (restaurants[i].Name.Equals(name))
                    {
                        //Sets description
                        Description.Text = restaurants[i].Description;

                        //Sets website url if it exists
                        if (restaurants[i].Url.Length > 7)
                        {
                            Uri websiteUri = new Uri(restaurants[i].Url);
                            Website.NavigateUri = websiteUri;
                        }
                        else
                        {
                            Website.Content = "Website not available.";
                        }

                        //Finds day of the week, and sets hours textbox accordingly
                        if ((int)DateTime.Today.DayOfWeek == 0)
                        {
                            Hours.Text = "Sunday Hours: " + restaurants[i].Sunday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 1)
                        {
                            Hours.Text = "Monday Hours: " + restaurants[i].Monday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 2)
                        {
                            Hours.Text = "Tuesday Hours: " + restaurants[i].Tuesday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 3)
                        {
                            Hours.Text = "Wednesday Hours: " + restaurants[i].Wednesday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 4)
                        {
                            Hours.Text = "Thursday Hours: " + restaurants[i].Thursday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 5)
                        {
                            Hours.Text = "Friday Hours: " + restaurants[i].Friday_hours.Replace(",", "-");
                        }
                        else if ((int)DateTime.Today.DayOfWeek == 6)
                        {
                            Hours.Text = "Saturday Hours: " + restaurants[i].Saturday_hours.Replace(",", "-");
                        }
                    }
                }
            }
        }
    }
}