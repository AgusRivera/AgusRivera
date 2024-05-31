using AMVTravels.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMVTravels.Contexts
{
    public class IdentityDBContext : IdentityDbContext <User, Role, string>
    {
        public IdentityDBContext( DbContextOptions<IdentityDBContext> options) : base(options)
        {
            
        }
    }
}
