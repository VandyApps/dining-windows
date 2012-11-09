using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics; //---for Debug.WriteLine()---
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
using Microsoft.Phone.Controls.Maps;

namespace Windows_Dining
{
    public partial class MapDetail : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
       // bool MapSet = false;

        public MapDetail()
        {
            InitializeComponent();

            //sets up GeoCoordinateWatcher
            if (watcher == null)
            {
                //---get the highest accuracy---
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High)
                {
                    //---the minimum distance (in meters) to travel before the next 
                    // location update---
                    MovementThreshold = 10
                };

                //---event to fire when a new position is obtained---
                watcher.PositionChanged += new
                EventHandler<GeoPositionChangedEventArgs
                <GeoCoordinate>>(watcher_PositionChanged);

                //---event to fire when there is a status change in the location 
                // service API---
                watcher.StatusChanged += new
                EventHandler<GeoPositionStatusChangedEventArgs>
                (watcher_StatusChanged);
                watcher.Start();
            }

            setMap();


            

        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    Debug.WriteLine("disabled");
                    break;

                case GeoPositionStatus.Initializing:
                    Debug.WriteLine("initializing");
                    break;

                case GeoPositionStatus.NoData:
                    Debug.WriteLine("nodata");
                    break;

                case GeoPositionStatus.Ready:
                    Debug.WriteLine("ready");
                    break;
            }
        }

        public void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Debug.WriteLine("(Coordinates: {0},{1})",
            e.Position.Location.Latitude, e.Position.Location.Longitude);



            DiningMap.Center = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
            DiningMap.ZoomLevel = 10;
            DiningMap.ZoomBarVisibility = System.Windows.Visibility.Visible;
       
        }

        public void setMap()
        {
            
            IList<Dining1> restaurants = DBHelper.GetRestaurants();           // Access Dining1 Database
            //float[,] latLongArray = new float[restaurants.Count(), 2];    // Create 2 dimensional array to hold latitude and longitude data
            /*Temporary Solution For GeoCoordinate Array.*/
            double[,] latLongArray = {
                                        {36.146405, -86.803178}, //1
                                        {36.141951, -86.797127},
                                        {36.141771, -86.79694},
                                        {36.146321, -86.803037},
                                        {36.146545, -86.803471}, //5
                                        {36.146626, -86.803736},
                                        {36.147418, -86.806839},
                                        {36.147461, -86.806705},
                                        {36.140584, -86.806286},
                                        {36.144847, -86.805728}, //10
                                        {36.145718, -86.800529},
                                        {36.144785, -86.806399},
                                        {36.129392, -86.778613},
                                        {36.144801, -86.802836}
                                    
            
                                    };

            Pushpin[] restaurantLocations = new Pushpin[restaurants.Count()];   // Create array to hold pushpins
            MapLayer[] pushpinLayers = new MapLayer[restaurants.Count()];           

            // Fill Pushpin array with Pushpin Objects 
            for (int i = 0; i < restaurantLocations.Count(); i++)
                {
                    restaurantLocations[i] = new Pushpin();
                }
            

            for (int row = 0; row < 14/*(14 is number in test array) restaurants.Count()*/; row++)
                {
                    //Debug.WriteLine("id: {0} {1},{2}", restaurants[row].Unique_id, restaurants[row].Lat, restaurants[row].Long);
                    latLongArray[row, 0] = latLongArray[row, 0];//(float) restaurants[row].Lat;
                    latLongArray[row, 1] = latLongArray[row, 1];//(float) restaurants[row].Long;
  

                    
                    restaurantLocations[row].Location = new GeoCoordinate(latLongArray[row, 0],latLongArray[row, 1]);
                    Debug.WriteLine(restaurants[row].Name + ": " + latLongArray[row,0] + "," + latLongArray[row,1]);

                    DiningMap.Children.Add(restaurantLocations[row]);

                }

           


        }

    }
}