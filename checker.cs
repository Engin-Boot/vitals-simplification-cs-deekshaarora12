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

            internal vitals(string name, float upperlimit, float lowerlimit)
            {
                this.name = name;
                this.upperlimit = upperlimit;
                this.lowerlimit = lowerlimit;
               
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
                return ((value >= lowerlimit && value <= upperlimit));
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
        static void ExpectTrue(bool expression)
        {
            if (!expression)
            {
                Console.WriteLine("Expected true, but got false");
                Environment.Exit(1);
            }
        }
        static void ExpectFalse(bool expression)
        {
            if (expression)
            {
                Console.WriteLine("Expected false, but got true");
                Environment.Exit(1);
            }
        }
        static void Main()
        {
            AlertInSMS alertsms = new AlertInSMS();
            AlertInSound alertsound = new AlertInSound();

            vitals bpm = new vitals("Bpm", 150, 70);
            bpm.CheckVitals(alertsms, "Bpm", 40);
            bpm.CheckVitals(alertsound, "Bpm", 170);
            bpm.CheckVitals(alertsound, "Bpm", 150);

            ExpectTrue(bpm.checkHigh("Bpm",190));
            ExpectTrue(bpm.checkLow("Bpm", 19));
            ExpectTrue(bpm.checkNormal("Bpm", 70));

            ExpectFalse(bpm.checkHigh("Bpm", 17));
            ExpectFalse(bpm.checkLow("Bpm", 120));
            ExpectFalse(bpm.checkNormal("Bpm", 12));

            vitals resp = new vitals("Resp", 95, 30);
            resp.CheckVitals(alertsms, "Resp", 14);
            resp.CheckVitals(alertsound, "Resp", 170);
            resp.CheckVitals(alertsound, "Resp", 30);

            ExpectTrue(resp.checkHigh("Resp", 96));
            ExpectTrue(resp.checkLow("Resp", 29));
            ExpectTrue(resp.checkNormal("Resp", 30));

            ExpectFalse(resp.checkHigh("Resp", 17));
            ExpectFalse(resp.checkLow("Resp", 30));
            ExpectFalse(resp.checkNormal("Resp", 120));

            vitals spo2 = new vitals("Spo2", 90, 90);
            spo2.CheckVitals(alertsms, "Spo2", 14);
            spo2.CheckVitals(alertsound, "Spo2", 120);
            spo2.CheckVitals(alertsound, "Spo2", 90);

            ExpectTrue(spo2.checkHigh("Spo2", 109));
            ExpectTrue(spo2.checkLow("Spo2", 45));
            ExpectTrue(spo2.checkNormal("Spo2", 90));

            ExpectFalse(spo2.checkHigh("Spo2", 17));
            ExpectFalse(spo2.checkLow("Spo2", 90));
            ExpectFalse(spo2.checkNormal("Spo2", 120));



        }
    }
}
