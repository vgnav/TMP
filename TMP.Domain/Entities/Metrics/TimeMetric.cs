
namespace TMP.Domain.Entities.Metrics
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class TimeMetric : CardioMetric
    {
        public long MilliSeconds { get; set; }
        
        [NotMapped]
        public decimal Seconds
        {
            get
            {
                return MilliSeconds / 1000;
            }
        }      
    }
}
