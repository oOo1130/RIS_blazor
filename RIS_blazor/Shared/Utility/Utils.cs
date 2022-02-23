using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIS_blazor.Shared.Utility
{
    public static class Utils
    {
        //static string sql = string.Empty;
        //static SqlConnection con;
        //static SqlCommand cmd;

        //static string _server = @"115.69.214.82;";
        static string _server = @"EMSL;";
        public static  DateTime GetServerDateAndTime()
        {
            return DateTime.Now;
            
        }

        public static string GetLegacyDbConnectionString()
        {

            return "Data Source=" + _server + "Initial Catalog=DicomServer;" + "Persist Security Info=False;User ID=sa;Password=EmslDicomServer@2021;";

        }


        
    }
}
