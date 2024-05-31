using Microsoft.AspNetCore.Identity;

namespace AMVTravels.Models.Entities.Identity
{
    public class User : IdentityUser
    {
        public string BusinessArea { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime EgressData { get; set; }
    }
}
