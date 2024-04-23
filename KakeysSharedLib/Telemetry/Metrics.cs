using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysSharedLib.Telemetry;

public class Metrics
{
    public static string Name = "";
    public static Meter DashboardMeter = new(Name, "1.0.0");

    public static Counter<int> homePageAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");
    public static Histogram<int> histogramGreetings = DashboardMeter.CreateHistogram<int>("orderpage.histogram");
}
