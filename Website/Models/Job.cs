using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models;

public partial class Job
{
    public int Id { get; set; }

    [StringLength(255)]
    public string? Company { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(100)]
    public string? Dates { get; set; }

    [StringLength(50)]
    public string? StartDate { get; set; }

    [StringLength(50)]
    public string? EndDate { get; set; }

    [StringLength(255)]
    public string? Role { get; set; }

    public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();

    [NotMapped]
    public string StartAndEndDate
    {
        get
        {
            string sStartDate = ToMonthYear(StartDate ?? "");
            if (string.IsNullOrEmpty(EndDate))
            {
                // Say end date is "Present"
                return $"{sStartDate} - Present";
            }
            else if (StartDate == EndDate)
            {
                // 1 month job. 
                return $"{sStartDate}";
            }            
            else
            {
                return $"{sStartDate} - {ToMonthYear(EndDate)}";
            }
        }
    }

    [NotMapped]
    public int Duration
    {
        get
        {
            DateTime dStartDate;
            dStartDate = DateTime.Parse(StartDate);
            DateTime dEndDate;
            int totalMonths = 0;


            if (string.IsNullOrEmpty(EndDate))
            {
                // End date is now, so calculate duration from start date to now
                dEndDate = DateTime.Now;
                totalMonths = ((dEndDate.Year - dStartDate.Year) * 12) + (dEndDate.Month - dStartDate.Month) + 1;
            }
            else if (StartDate == EndDate)
            {
                // 1 month job. 
                totalMonths = 1;
            }
            else
            {
                dEndDate = DateTime.Parse(EndDate);
                totalMonths = ((dEndDate.Year - dStartDate.Year) * 12) + (dEndDate.Month - dStartDate.Month) + 1;
            }

            return totalMonths;
        }
    }
    string ToMonthYear(string pYearMonth)
    {
        // Converts YYYY-MM to "Month Year" format, e.g., "2024-01" to "January 2024"
        string sYearMonth = pYearMonth.Trim();
        DateTime dYearMonth = DateTime.Parse(sYearMonth);
        return dYearMonth.ToString("MMMM yyyy");
    }

}

