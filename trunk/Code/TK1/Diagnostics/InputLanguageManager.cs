using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace TK1.Diagnostics
{
    public class KeyboardInputLanguageManager
    {
        [DllImport("user32.dll")]
        private static extern bool GetKeyboardLayoutName(StringBuilder pwszKLID);
        private const int KL_NAMELENGTH = 9;

        public static CultureInfo CultureOfCurrentLayout()
        {
            StringBuilder sb = new StringBuilder(KL_NAMELENGTH);

            if (GetKeyboardLayoutName(sb))
            {
                int klid = int.Parse(
                   sb.ToString().Substring(0, KL_NAMELENGTH - 1),
                   NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);

                // strip all but the bottom half of the number
                klid &= 0xffff;

                return new CultureInfo(klid, false);
            }

            return (null);
        }
    }
}
