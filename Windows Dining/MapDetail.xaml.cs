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
                                        {36.141951, -86.797127}, //2
                                        {36.141771, -86.79694},  //3
                                        {36.146321, -86.803037}, //4
                                        {36.146545, -86.803471}, //5
                                        {36.146626, -86.803736}, //6
                                        {36.147418, -86.806839}, //7
                                        {36.147461, -86.806705}, //8
                                        {36.140584, -86.806286}, //9
                                        {36.144847, -86.805728}, //10
                                        {36.145718, -86.800529}, //11
                                        {36.144785, -86.806399}, //12
                                        {36.129392, -86.778613}, //13
                                        {36.144801, -86.802836}, //14
                                        {36.13863, -86.805874},  //15
                                        {36.14654, -86.800662},  //16
                                        {36.148782, -86.803473}, //17
                                        {36.143796, -86.803227}, //18
                                        {36.144929, -86.805791}, //19
                                        {36.147903, -86.805857}, //20
                                        {36.140609, -86.806328}, //21
                                        {36.146587, -86.803428}, //22
                                        {36.149829, -86.80124},  //23
                                        {36.158751, -86.818717}, //24
                                        {36.146313, -86.808728}, //25
                                        {36.148542, -86.799112}, //26
                                        {36.137065, -86.799215}, //27
                                        {36.151832, -86.805135}, //28
                                        {36.148,    -86.8084  }, //29
                                        {36.148266, -86.806459}, //30
                                        {36.1368,   -86.7996  }, //31
                                        {36.14267,  -86.7992  }, //32
                                        {36.151744, -86.803774}, //33
                                        {36.1464,   -86.809   }, //34
                                        {36.150167, -86.797837}, //35
                                        {36.1513,   -86.804124}, //36
                                        {36.148439, -86.806092}, //37
                                        {36.1488,   -86.805   }, //38
                                        {36.13715,  -86.8009  }, //39
                                        {36.150282, -86.800717}, //40
                                        {36.148327, -86.807474}, //41
                                        {36.136348, -86.801419}, //42
                                        {36.148368, -86.807427}, //43
                                        {36.147596, -86.807077}, //44
                                        {36.148299, -86.807506}, //45
                                        {36.136725, -86.799483}, //46
                                        {36.1507,   -86.801198}, //47
                                        {36.14859,  -86.799095}, //48
                                        {36.145667, -86.810038}, //49
                                        {36.13675,  -86.8025  }  //50
                                    
            
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