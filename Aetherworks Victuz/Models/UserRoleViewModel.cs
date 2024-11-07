using Microsoft.AspNetCore.Identity;

namespace Aetherworks_Victuz.Models
{
    public class UserRoleViewModel
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
        public User user { get; set; }
    }
}
