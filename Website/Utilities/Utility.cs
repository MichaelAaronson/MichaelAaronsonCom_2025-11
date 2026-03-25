using System.Configuration;

namespace Website.Utilities
{
    public static class Utility
    {
        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToMonthYear(string pYearMonth)
        {
            // Converts YYYY-MM to "Month Year" format, e.g., "2024-01" to "January 2024"
            string sYearMonth = pYearMonth.Trim();
            DateTime dYearMonth = DateTime.Parse(sYearMonth);
            return dYearMonth.ToString("MMMM yyyy");
        }
           
    }
}
