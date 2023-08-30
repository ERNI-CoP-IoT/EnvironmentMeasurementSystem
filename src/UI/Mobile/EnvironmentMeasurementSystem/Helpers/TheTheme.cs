using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentMeasurementSystem.Helpers;
public static class TheTheme
{
    public static void SetTheme()
    {
        switch (Settings.Theme)
        {
            //default
            case 0:
                App.Current.UserAppTheme = AppTheme.Unspecified;
                break;
            //light
            case 1:
                App.Current.UserAppTheme = AppTheme.Light;
                break;
            //dark
            case 2:
                App.Current.UserAppTheme = AppTheme.Dark;
                break;
        }
        
    }
}
