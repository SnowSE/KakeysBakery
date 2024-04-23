using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysSharedLib.Telemetry;

public class Metrics
{
    public static string Name = "KakeyMetrics";
    public static Meter DashboardMeter = new(Name, "1.0.0");

    public static Counter<int> homePageAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");
    public static Histogram<int> orderPageLoadTimes = DashboardMeter.CreateHistogram<int>("orderpage.histogram");

    public static Counter<int> OrderAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");
    public static Counter<int> CustomizeAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");
    public static Counter<int> AboutUsAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");
    public static Counter<int> CartAccessCount = DashboardMeter.CreateCounter<int>("homepage.count");

}