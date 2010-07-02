using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace TK1.Dev
{
    public class SetLocale
    {
        // The name of a country or region in English

        const int LOCALE_SNAME = 0x1002;
        const int LOCALE_SENGLISHCOUNTRYNAME = 0x1002;
        const int LOCALE_SENGLISHLANGUAGENAME = 0x1002;

        //static extern int SetLocaleInfo(int LOCALE_SYSTEM_DEFAULT, int LOCALE_SSHORTDATE, string lpLCData);

        public const int LOCALE_STIMEFORMAT = 0x1003;

        public const int LOCALE_SYSTEM_DEFAULT = 0x800;

        public const int LOCAL_SSHORTDATE = 0x31;

        // Use COM interop to call the Win32 API GetLocalInfo.
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetLocaleInfo(
            // The locale identifier.
           int Locale,
            // The information type.
           int LCType,
            // The buffer size.
           [In, MarshalAs(UnmanagedType.LPWStr)] string lpLCData, int cchData
         );
        //public static extern int SetLocaleInfo(
        //    // The locale identifier.
        //    int Locale,
        //    // The information type.
        //    int LCType,
        //    // The buffer size.
        //    [In, MarshalAs(UnmanagedType.LPWStr)] string lpLCData, int cchData
        //    );

        [DllImport("kernel32.dll")]
        static extern bool SetLocaleInfo(uint Locale, uint LCType, string lpLCData);

        // A method to retrieve the .NET Framework Country/Region
        // that maps to the specified CultureInfo.
        public static String GetNetCountryRegionName(CultureInfo ci)
        {
            // If the specified CultureInfo represents a specific culture,
            // the attempt to create a RegionInfo succeeds.
            try
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                return ri.EnglishName;
            }
            // Otherwise, the specified CultureInfo represents a neutral
            // culture, and the attempt to create a RegionInfo fails.
            catch
            {
                return String.Empty;
            }
        }

        // A method to retrieve the Win32 API Country/Region
        // that maps to the specified CultureInfo.
        public static String GetWinCountryRegionName(CultureInfo ci)
        {
            int size = GetLocaleInfo(ci.LCID, LOCALE_SENGLISHCOUNTRYNAME, null, 0);
            String str = new String(' ', size);
            int err = GetLocaleInfo(ci.LCID, LOCALE_SENGLISHCOUNTRYNAME, str, size);
            // If the string is not empty, GetLocaleInfo succeeded.
            // It will succeed regardless of whether ci represents
            // a neutral or specific culture.
            if (err != 0)
                return str;
            else
                return String.Empty;
        }

        // A method to retrieve the Win32 API Country/Region
        // that maps to the specified CultureInfo.
        public static void SetWinCountryRegionName(CultureInfo ci)
        {
            SetLocaleInfo((uint)ci.LCID, LOCAL_SSHORTDATE, "yyyy-MM-dd");
            SetLocaleInfo((uint)ci.LCID, LOCALE_STIMEFORMAT, "HH:mm:ss");

            //SetLocaleInfo(LOCALE_SYSTEM_DEFAULT, LOCALE_STIMEFORMAT, "HH:mm:ss");

            //int size = SetLocaleInfo(ci.LCID, LOCALE_SENGLISHCOUNTRYNAME, null, 0);
            //String str = new String(' ', size);
            //int err = GetLocaleInfo(ci.LCID, LOCALE_SENGLISHCOUNTRYNAME, str, size);
            //// If the string is not empty, GetLocaleInfo succeeded.
            //// It will succeed regardless of whether ci represents
            //// a neutral or specific culture.
            //if (err != 0)
            //    return str;
            //else
            //    return String.Empty;

        }

    }
}
