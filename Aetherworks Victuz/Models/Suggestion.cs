using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aetherworks_Victuz.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public IdentityUser? User { get; set; }

    }
}
