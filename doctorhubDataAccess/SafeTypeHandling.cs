using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubDataAccess
{
    public class SafeTypeHandling
    {
        public static  string ConvertToString (object value)
        {
            if(value == null || value == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(value).Trim();
            }
        }


        public static Double ConvertToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }


        public static DateTime ConvertToDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return DateTime.Parse(value.ToString());
            }
        }

        public static Int32 ConvertToInt32(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Int32.Parse(value.ToString());
            }
        }

        public static Int32 ConvertStringToInt32(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Int32.Parse(value.ToString());
            }
        }


        public static bool ConvertStringToBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }
            if(string.Compare("true",value.ToString().ToLower(),true) ==0
                || string.Compare("yes", value.ToString().ToLower(), true) == 0
                || string.Compare("1", value.ToString().ToLower(), true) == 0
                || string.Compare("y", value.ToString().ToLower(), true) == 0
                )
            {
                return true;
            }
            return false;
        }


    }


}
