using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSearcher
{
    //FOR DELETE
    public class Dijkstra
    {
        List<Station> stationWithValues;
        Station begStation;
        Station destStation;
        public Dijkstra(List<Station> stations, int sID, int dID)
        //Preparation for Dijkstra Algorithm
        {
            stationWithValues = new List<Station>();
            foreach (Station s in stations)
            {
                stationWithValues.Add(s);
                s.value = int.MaxValue;
                if (sID == s.stationID)
                    begStation = s;
                if (dID == s.stationID)
                    destStation = s;
            }
        }
        public static List<Station> DijkstraAlgorithm(int sID, int dID, List<Station> stations)
        //Non-recursive part of the Dijkstra Algorithm.
        //sID and dID means source/destination station's ID
        //returns the shortest path between sID and dID
        {
            Dijkstra d = new Dijkstra(stations, sID, dID);
            d.begStation.value = 0;
            List<Station> finalPath = new List<Station>();
            List<Station> currentPath = new List<Station>();
            currentPath.Add(d.begStation);
            finalPath = DijkstraAlgorithmReccursive(d.begStation, d.destStation, currentPath, finalPath);
            return finalPath;
        }

        private static List<Station> DijkstraAlgorithmReccursive(Station source, Station dest, List<Station> currentPath, List<Station> finalPath)
        {
            if (source == dest)
            {
                //ending point
                finalPath.Clear();
                foreach (var r in currentPath)
                    finalPath.Add(r);

                return finalPath;
            }
            foreach (var v in source.neighbours)
            {
                int alt = source.value + v.Item2;
                if (alt < v.Item1.value)
                {
                    v.Item1.value = alt;

                    currentPath.Add(v.Item1);
                    finalPath = DijkstraAlgorithmReccursive(v.Item1, dest, currentPath, finalPath);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
            return finalPath;
        }

        public static List<(List<Station>, int)> AnalyseTheResult(List<Station> stations)
        //The function that splits the list of stations into the list of lists of stations where stations are on the same line
        {
            List<(List<Station>, int)> lists = new List<(List<Station>, int)>();
            int lastConnection = -1;
            ValueTuple<Station, int, int> toSave = new ValueTuple<Station, int, int>();
            bool toSaveFlag;
            bool toSaveFlag2;
            for (int i = 0; i < stations.Count - 1; i++)
            {
                toSaveFlag = true;
                toSaveFlag2 = false;
                foreach (var neigbour in stations[i + 1].neighbours)
                {
                    if (neigbour.Item1.stationID == stations[i].stationID)
                    //found the neighbour
                    {
                        if (neigbour.Item3 == lastConnection)
                        //on the same line as the last station
                        {
                            lists.Find(x => x.Item2 == lastConnection).Item1.Add(neigbour.Item1);
                            toSaveFlag = false;
                            break;
                        }
                        else
                        {
                            toSaveFlag2 = true;
                            toSave = neigbour;
                        }
                    }
                }
                if (toSaveFlag == true && toSaveFlag2 == true)
                //starting a new line
                {
                    if (lists.Exists(x => x.Item2 == lastConnection)) lists.Find(x => x.Item2 == lastConnection).Item1.Add(toSave.Item1);//not optimal!!!
                    var item = new ValueTuple<List<Station>, int>();
                    item.Item1 = new List<Station>();
                    item.Item1.Add(toSave.Item1);
                    item.Item2 = toSave.Item3;
                    lists.Add(item);
                    lastConnection = toSave.Item3;
                }

            }
            lists.Find(x => x.Item2 == lastConnection).Item1.Add(stations.Last());//adding last station
            return lists;
        }

    }
}
