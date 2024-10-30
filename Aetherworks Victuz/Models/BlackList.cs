using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aetherworks_Victuz.Models
{
    public class BlackList
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public IdentityRole? Role { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
