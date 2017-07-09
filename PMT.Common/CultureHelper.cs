using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PMT.Common
{
    public static class CultureHelper
    {
        private static Dictionary<string,string> implementedDisplayLanguages = new Dictionary<string,string> {
            { "en-US", "English"},
            { "el","Greek (Ελληνικά)" }
        };

        public static Dictionary<string, string> GetImplementedDisplayLanguages()
        {
            return implementedDisplayLanguages;
        }

        public static string GetImplementedCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture();

            if (implementedDisplayLanguages.Keys.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name;

            var n = GetNeutralCulture(name);
            foreach (var c in implementedDisplayLanguages.Keys)
                if (c.StartsWith(n))
                    return c;
            //return GetDefaultCulture(); 
            return name;
        }

        public static string GetDefaultCulture()
        {
            return implementedDisplayLanguages.Keys.FirstOrDefault(); 
        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-').FirstOrDefault();
        }
    }
}
