﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Benchmarks
{
    public class DVRPData
    {
        /******************************************************************/
        /******************* PROPERTIES, PRIVATE FIELDS *******************/
        /******************************************************************/
        /* private ulong dataID;

         public ulong DataID
         {

         }

         private static ulong idSequence = 0;

         public static ulong id
         {
             get { return idSequence; }
             set { idSequence = value; }
         }

         public static ulong getID()
         {
             return idSequence++;
         }*/

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int num_depots;

        public int Num_Depots
        {
            get { return num_depots; }
            set { num_depots = value; }
        }

        private int num_capacities;

        public int Num_Capacities
        {
            get { return num_capacities; }
            set { num_capacities = value; }
        }

        private int num_visits;

        public int Num_Visits
        {
            get { return num_visits; }
            set { num_visits = value; }
        }

        private int num_locations;

        public int Num_Locations
        {
            get { return num_locations; }
            set { num_locations = value; }
        }

        private int num_vehicles;

        public int Num_Vehicles
        {
            get { return num_vehicles; }
            set { num_vehicles = value; }
        }

        private int capacites;

        public int Capacites
        {
            get { return capacites; }
            set { capacites = value; }
        }

        /******************************************************************/
        /************************** DATA SECTION **************************/
        /******************************************************************/

        /// <summary>
        ///     IDs of depots
        /// </summary>
        private int[] depots_ids;

        public int[] Depots_IDs
        {
            get { return depots_ids; }
            set { depots_ids = value; }
        }

        /// <summary>
        ///     How much capacitie is taken from each location
        /// </summary>
        private int[] demands;

        public int[] Demands
        {
            get { return demands; }
            set { demands = value; }
        }

        private double speed;

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        /// <summary>
        ///     Locations coded as follows:
        ///     [i] = [0][1] ... [n]
        ///     where i is the location id
        ///     and [0][1] ... [n] is the location in euclidean space (e.g. x y )
        /// </summary>
        private int[][] location_coord;

        public int[][] Location_Coord
        {
            get { return location_coord; }
            set { location_coord = value; }
        }

        /// <summary>
        ///     Simply map for depots' ids
        /// </summary>
        private int[] depot_location;

        public int[] Depot_Location
        {
            get { return depot_location; }
            set { depot_location = value; }
        }

        /// <summary>
        ///     Simply map for clients' ids
        ///     usually: [i] = i
        /// </summary>
        private int[] visit_location;

        public int[] Visit_Location
        {
            get { return visit_location; }
            set { visit_location = value; }
        }

        /// <summary>
        ///     How long does each client take to unload
        /// </summary>
        private int[] duration;

        public int[] Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        /// <summary>
        ///     When each depot starts and ends:
        ///     [depot_id] = [0][1],
        ///     where [0] = start and [1] = end
        /// </summary>
        private int[][] depot_time_window;

        public int[][] Depot_Time_Window
        {
            get { return depot_time_window; }
            set { depot_time_window = value; }
        }

        /// <summary>
        ///     When each client sent a request
        /// </summary>
        private int[] time_avail;

        public int[] Time_Avail
        {
            get { return time_avail; }
            set { time_avail = value; }
        }


        /******************************************************************/
        /************************** CONSTRUCTORS **************************/
        /******************************************************************/

        //public DVRPData()
        public DVRPData(string file)
        {
            //this.id = getID();
            parse(file);
        }

        public DVRPData(
            string _name,
            int _numDepots,
            int _numCapacities,
            int _numVisits,
            int _numLocations,
            int _numVehicles,
            int _capacities,
            int[] _depotIDs,
            int[] _demands,
            int[][] _locationCoord,
            int[] _depotLocation,
            int[] _visitLocation,
            int[] _duration,
            int[][] _depotTimeWindow,
            int[] _timeAvail,
            double _speed
            )
        {
            this.Name = _name;
            this.Num_Depots = _numDepots;
            this.Num_Capacities = _numCapacities;
            this.Num_Visits = _numVisits;
            this.Num_Locations = _numLocations;
            this.Num_Vehicles = _numVehicles;
            this.Capacites = _capacities;
            this.Depots_IDs = _depotIDs;
            this.Demands = _demands;
            this.Location_Coord = _locationCoord;
            this.Depot_Location = _depotLocation;
            this.Visit_Location = _visitLocation;
            this.Duration = _duration;
            this.Depot_Time_Window = _depotTimeWindow;
            this.Time_Avail = _timeAvail;
            this.Speed = _speed;
        }

        /*******************************************************************/
        /************************ PRIVATE METHODS **************************/
        /*******************************************************************/

        private void parse(string file)
        {

        }

        /*******************************************************************/
        /************************* PUBLIC METHODS **************************/
        /*******************************************************************/

        #region SERIALIZATION
        public static DVRPData Deserialize(string from)
        {
            string _name;
            int _numDepots;
            int _numCapacities;
            int _numVisits;
            int _numLocations;
            int _numVehicles;
            int _capacities;
            int[] _depotIDs;//
            int[] _depotVehicleNo;//TODO: implement later
            int[] _demands;//
            int[][] _locationCoord;//
            int[] _depotLocation;//
            int[] _visitLocation;//
            int[] _duration;//
            int[][] _depotTimeWindow;//
            int[] _timeAvail;//
            double _speed;

            string[] splitPiece = from.Split('|');

            List<int[]> __locationCoord = new List<int[]>();

            string[] sClients = splitPiece[0].Split('@');

            List<int> __demands = new List<int>();
            List<int> __visitLocations = new List<int>();
            List<int> __durations = new List<int>();
            List<int> __timeAvailable = new List<int>();

            int locationsCount = 0;
            for (int j = 0; j < sClients.Length; j += 1)
            {
                string clientInfo = sClients[j];
                string[] sClientData = clientInfo.Split('#');
                __timeAvailable.Add((int)double.Parse(sClientData[0]));
                __demands.Add(int.Parse(sClientData[1])); 
                __durations.Add ( (int) double.Parse ( sClientData [ 2 ] ));
                string [] sLocationData = sClientData[4].Split('%');
                __visitLocations.Add(locationsCount);
                __locationCoord.Add(new int[2] { int.Parse(sLocationData[0]), int.Parse(sLocationData[1]) });
                locationsCount++;
            }

            string[] sDepots = splitPiece[1].Split('@');

            List<int> __depotIDs = new List<int>();
            List<int> __depotLocations = new List<int>();
            List<int[]> __depotTimeWindow = new List<int[]>();

            int depotIDCount = 0;
            for (int j = 0; j < sDepots.Length; j += 1)
            {
                string depotInfo = sDepots[j];
                string[] sDepotData = depotInfo.Split('^');

                string[] sWorkingHoursData = sDepotData[0].Split('_');
                __depotTimeWindow.Add(new int[2] { (int)double.Parse(sWorkingHoursData[0]), (int)double.Parse(sWorkingHoursData[1]) });

                __depotIDs.Add(depotIDCount);
                __depotLocations.Add(locationsCount);

                string[] sLocationData = sDepotData[1].Split('_');
                __locationCoord.Add(new int[2] { int.Parse(sLocationData[0]), int.Parse(sLocationData[1]) });
                locationsCount++;

            }

            string[] sVehicles = splitPiece[2].Split('@');
            _numVehicles = sVehicles.Length;

            _name = "Hurr durr";
            _numDepots = __depotIDs.Count;
            _numCapacities = 1;
            _numVisits = __visitLocations.Count;
            _numLocations = __depotIDs.Count + __visitLocations.Count;
            _speed = double.Parse(splitPiece[3]);

            _locationCoord = __locationCoord.ToArray();

            _demands = __demands.ToArray();
            _visitLocation = __visitLocations.ToArray();
            _duration = __durations.ToArray();
            _timeAvail = __timeAvailable.ToArray();

            _depotIDs = __depotIDs.ToArray();
            _depotLocation = __depotLocations.ToArray();
            _depotTimeWindow = __depotTimeWindow.ToArray();

            _capacities = (int)double.Parse(splitPiece[4]);

            return new DVRPData(_name, _numDepots, _numCapacities, _numVisits,
                _numLocations, _numVehicles, _capacities, _depotIDs, _demands,
                _locationCoord, _depotLocation, _visitLocation, _duration,
                _depotTimeWindow, _timeAvail , _speed );

        }

        public string Serialize()
        {
            StringBuilder serializedDVRPdataBuilder = new StringBuilder();



            for (int i = 0; i < this.Visit_Location.Length; i += 1)
            {
                if (i != 0)
                {
                    serializedDVRPdataBuilder.Append('@');
                }
                serializedDVRPdataBuilder.Append(this.Time_Avail[i]);
                serializedDVRPdataBuilder.Append('#');
                serializedDVRPdataBuilder.Append(this.Demands[i]);
                serializedDVRPdataBuilder.Append('#');
                serializedDVRPdataBuilder.Append(this.Duration[i]);
                serializedDVRPdataBuilder.Append('#');
                serializedDVRPdataBuilder.Append(false);
                serializedDVRPdataBuilder.Append('#');
                serializedDVRPdataBuilder.Append(this.Location_Coord[this.Visit_Location[i]][0]);
                serializedDVRPdataBuilder.Append('%');
                serializedDVRPdataBuilder.Append(this.Location_Coord[this.Visit_Location[i]][1]);
            }
            serializedDVRPdataBuilder.Append('|');
            for (int i = 0; i < this.Depot_Location.Length; i += 1)
            {
                if (i != 0)
                {
                    serializedDVRPdataBuilder.Append('@');
                }
                serializedDVRPdataBuilder.Append(this.Depot_Time_Window[i][0]);
                serializedDVRPdataBuilder.Append('_');
                serializedDVRPdataBuilder.Append(this.Depot_Time_Window[i][1]);
                serializedDVRPdataBuilder.Append('^');
                serializedDVRPdataBuilder.Append(this.Location_Coord[this.Depot_Location[i]][0]);
                serializedDVRPdataBuilder.Append('_');
                serializedDVRPdataBuilder.Append(this.Location_Coord[this.Depot_Location[i]][1]);                
            }
            serializedDVRPdataBuilder.Append('|');
            for (int i = 0; i < this.Num_Vehicles; i += 1)
            {
                if (i != 0)
                {
                    serializedDVRPdataBuilder.Append('@');
                }
                serializedDVRPdataBuilder.Append("hurr durr");
                serializedDVRPdataBuilder.Append('_');
                serializedDVRPdataBuilder.Append("hurr durr");
                serializedDVRPdataBuilder.Append('^');
                serializedDVRPdataBuilder.Append("hurr durr");
                serializedDVRPdataBuilder.Append('_');
                serializedDVRPdataBuilder.Append("hurr durr");  
                serializedDVRPdataBuilder.Append('/');
                serializedDVRPdataBuilder.Append(this.Capacites);
                serializedDVRPdataBuilder.Append('/');
                serializedDVRPdataBuilder.Append(this.Capacites);
                serializedDVRPdataBuilder.Append('/');
                serializedDVRPdataBuilder.Append(0);
            }

            serializedDVRPdataBuilder.Append('|');
            serializedDVRPdataBuilder.Append(this.Speed);
            serializedDVRPdataBuilder.Append('|');
            serializedDVRPdataBuilder.Append(this.Capacites);
            
            return serializedDVRPdataBuilder.ToString();

        }
        #endregion
    }
}
