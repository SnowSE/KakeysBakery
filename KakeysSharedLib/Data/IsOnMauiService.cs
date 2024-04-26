using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakeysSharedLib.Data;

public static class FeatureFlagService
{
    public static bool IsAvailable { get; set; }
    public static bool IsOnMauibb { get; set; }
    public static void SetVariable(bool value)
    {
        IsAvailable = value;
    }
    public static void SetVariable2(bool value)
    {
        IsOnMauibb = value;
    }
}