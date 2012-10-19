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
    /*public class DBHelper
    {
        private const string ConnectionString = "isostore:/dining.sdf";

        public static void CreateDatabase()
        {
            using (var context = new Dining(ConnectionString))
            {
                if (!context.DatabaseExists())
                {
                    // create database if it does not exist
                    context.CreateDatabase();
                }
            }
        }

        public static void DeleteDatabase()
        {
            using (var context = new Dining(ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    // delete database if it exist
                    context.DeleteDatabase();
                }
            }
        }

        public static IList<Dining1> GetRestaurants()
        {
            IList<Dining1> restaurants;
            using (var context = new Dining(ConnectionString))
            {
                restaurants = (from Dining1 d in context.Restaurants select d).ToList();
            }

            return restaurants;
        }

        public static void MoveReferenceDatabase()
        {
            // Obtain the virtual store for the application.
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();

            // Create a stream for the file in the installation folder.
            using (Stream input = Application.GetResourceStream(new Uri("ViewModels/dining.sdf", UriKind.Relative)).Stream)
            {
                // Create a stream for the new file in isolated storage.
                using (IsolatedStorageFileStream output = iso.CreateFile("dining.sdf"))
                {
                    // Initialize the buffer.
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;

                    // Copy the file from the installation folder to isolated storage. 
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        output.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
        }
    }
    */
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
                PageTitle.Text = name;
                for (int i = 0; i < restaurants.Count(); i++)
                {
                    if (restaurants[i].Name.Equals(name))
                    {
                        Description.Text = restaurants[i].Description;
                        Website.Text = "Website: " + restaurants[i].Url;
                        if ((int)DateTime.Today.DayOfWeek == 0)
                        {
                            Hours.Text = "Sunday Hours: " + restaurants[i].Sunday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 1)
                        {
                            Hours.Text = "Monday Hours: " + restaurants[i].Monday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 2)
                        {
                            Hours.Text = "Tuesday Hours: " + restaurants[i].Tuesday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 3)
                        {
                            Hours.Text = "Wednesday Hours: " + restaurants[i].Wednesday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 4)
                        {
                            Hours.Text = "Thursday Hours: " + restaurants[i].Thursday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 5)
                        {
                            Hours.Text = "Friday Hours: " + restaurants[i].Friday_hours;
                        }
                        if ((int)DateTime.Today.DayOfWeek == 6)
                        {
                            Hours.Text = "Saturday Hours: " + restaurants[i].Saturday_hours;
                        }
                    }
                }
            }
        }
    }
}