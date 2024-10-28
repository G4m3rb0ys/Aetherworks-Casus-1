using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aetherworks_Victuz.Models
{
    public class User
    {
        public int Id { get; set; }
        public int CredentialsId { get; set; }
        public IdentityUser? Credentials { get; set; }
        public ICollection<UserActivity>? UserActivities { get; set; }
    }
}
