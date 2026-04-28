using System.Web;
using System.Data.Entity;

namespace TestGitHubActions.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalLocation { get; set; }
    }
    
    public class HospitalDBContext : DbContext
    {
        public DbSet<Hospital> Hospitals { get; set; }
    }
}