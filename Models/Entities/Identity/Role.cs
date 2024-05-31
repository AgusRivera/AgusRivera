using Microsoft.AspNetCore.Identity;

namespace AMVTravels.Models.Entities.Identity
{
    public class Role : IdentityRole
    {
        public string Area { get; set; }
        public DateTime DischargeDate { get; set; }
    }
}
