/*using System;
using System.Diagnostics;

class Checker
{
    static bool vitalsAreOk(float bpm, float spo2, float respRate) {
        if(bpm < 70 || bpm > 150) {
            return false;
        } else if(spo2 < 90) {
            return false;
        } else if(respRate < 30 || respRate > 95) {
            return false;
        }
        return true;
    }
    static void ExpectTrue(bool expression) {
        if(!expression) {
            Console.WriteLine("Expected true, but got false");
            Environment.Exit(1);
        }
    }
    static void ExpectFalse(bool expression) {
        if(expression) {
            Console.WriteLine("Expected false, but got true");
            Environment.Exit(1);
        }
    }
    static int Main() {
        ExpectTrue(vitalsAreOk(100, 95, 60));
        ExpectFalse(vitalsAreOk(40, 91, 92));
        Console.WriteLine("All ok");
        return 0;
    }
}*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vitals_Simplification
{
 class Checker
    {

        internal abstract class Alert
        {
           internal abstract void GetAlert(string vitalname, Dictionary<string,bool> vitalalertinfo);
        }
        internal class AlertInSMS : Alert
        {
            internal override void GetAlert(string vitalname, Dictionary<string, bool> vitalalertinfo)
            {
                foreach (KeyValuePair<string, bool> kvp in vitalalertinfo)
                {
                    if (kvp.Value == true)
                        Console.WriteLine($"Alert in SMS --> {vitalname} {kvp.Key}");    
                }
            }
        }
        internal class AlertInSound : Alert
        {
            internal override void GetAlert(string vitalname, Dictionary<string, bool> vitalalertinfo)
            {
                foreach (KeyValuePair<string, bool> kvp in vitalalertinfo)
                {
                    if (kvp.Value == true)
                        Console.WriteLine($"Alert in Sound --> {vitalname} {kvp.Key}");
                }
            }
        }
       internal class vitals
        {
             string name;
             float upperlimit;
             float lowerlimit;
             float singlelimit;

            internal vitals(string name, float upperlimit, float lowerlimit, float singlelimit)
            {
                this.name = name;
                this.upperlimit = upperlimit;
                this.lowerlimit = lowerlimit;
                this.singlelimit = singlelimit;
            }


            public bool checkHigh(string vitalname,float value)
            {
                return (value > upperlimit);
            }
            public bool checkLow(string vitalname, float value)
            {
                return (value < lowerlimit);
            }
            public bool checkNormal(string vitalname, float value)
            {
                return ((value >= lowerlimit && value <= upperlimit)||(value == singlelimit));
            }

            public void CheckVitals(Alert alert,string vitalname,float value)
            {
                Dictionary<string, bool> vitalalertinfo = new Dictionary<string, bool>();

                bool high = checkHigh(vitalname, value);
                bool low = checkLow(vitalname, value);
                bool normal = checkNormal(vitalname, value);

                vitalalertinfo.Add("High rate", high);
                vitalalertinfo.Add("Low rate", low);
                vitalalertinfo.Add("Normal Rate", normal);

                alert.GetAlert(vitalname,vitalalertinfo);
            }
        }
        static void Main()
        {
            AlertInSMS alertsms = new AlertInSMS();
            AlertInSound alertsound = new AlertInSound();

            vitals vt = new vitals("Bpm", 150, 70, 0);
            vt.CheckVitals(alertsms, "Bpm", 40);
            vt.CheckVitals(alertsound, "Bpm", 170);

        }
    }
}
