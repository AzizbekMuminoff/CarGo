using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Controllers
{
    public static class DebugLogController
    {
        private static string log = "";

        public static void WriteLine(string t)
        {
            log += t + "\n";
        }

        internal static string GetLog()
        {
            return log;
        }
    }
}
