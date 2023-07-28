using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Provider;

namespace CarGo.Models
{
    public class GPSModel
    {
        private double Longitude;
        private double Latitude;
        private string CarID;
        private string dateStamp;
        public static string ParsingMode = "{DL} {SH} \n {ID} {D}";
        public static string[] ParseMode;

        private static double st= 0.0005;
        public GPSModel(double longitude, double latitude, string carID, string dateStamp)
        {
            Longitude = longitude;
            Latitude = latitude;
            CarID = carID;
            this.dateStamp = dateStamp;
        }

        public double Longitude1 { get => Longitude; set => Longitude = value; }
        public double Latitude1 { get => Latitude; set => Latitude = value; }
        public string CarID1 { get => CarID; set => CarID = value; }
        public string DateStamp { get => dateStamp; set => dateStamp = value; }

        public static GPSModel ParseGPS(string str)
        {
            double lat=0, lon=0;
            string id="", datestamp="";

            //Debug.WriteLine("String recieved: " + str);
            string[] pl = str.Split("\n");
            
            for(int i =0; i < pl.Length; i++)
            {
                int Ind = 0;

                for(int j =0; j < ParseMode[i].Length; j++)
                {
                    //Debug.WriteLine("Parsing start: ", pl[i] +  " < " + Ind);

                    if (pl[i][Ind] != ParseMode[i][j])
                    {
                        string strId = "";

                        if (ParseMode[i][j].Equals('{'))
                        {
                            for (; j < ParseMode[i].Length; j++)
                            {

                                Debug.Write(ParseMode[i][j]);

                                strId += ParseMode[i][j];
                                if (ParseMode[i][j].Equals('}'))
                                {
                                    break;
                                }
                            }

                        }
                        Debug.WriteLine("ID detected: " + strId);

                        string parseP = "";
                        bool ok = false;

                        for(; Ind < pl[i].Length; Ind++)
                        {
                            if (pl[i][Ind].Equals(' '))
                            {
                                ok = true;
                                break;
                            }
                            parseP += pl[i][Ind];
                        }
                        if (Ind == pl[i].Length)
                            ok = true;
                        Ind--;

                        Debug.WriteLine(parseP);

                        if (ok)
                        {
                            if (strId.Equals("{DL}"))
                            {
                                lat = Double.Parse(parseP);
                            }
                            else if (strId.Equals("{SH}"))
                            {
                                lon = Double.Parse(parseP);
                            }else if (strId.Equals("{ID}"))
                            {
                                id = parseP;
                            }
                            else
                            {
                                datestamp = parseP;
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Something is wrong with parse: " + parseP + " " + strId);
                        }
                    }

                    Ind++;
                }
            }
            Debug.WriteLine("Parsed: " + lon +" " + lat + " " + id + " "+ datestamp);
            return new GPSModel(lon, lat, id, datestamp);
        }
        override
        public string ToString()
        {
            string t = Longitude + "  " + Latitude + "  " + dateStamp; 
            return t;
        }
    }
}
