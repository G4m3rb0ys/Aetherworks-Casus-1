using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aetherworks_Victuz.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? CredentialId { get; set; }
        public IdentityUser? Credential { get; set; }
        public ICollection<Participation>? Participations { get; set; }
        public ICollection<Suggestion>? Suggestions { get; set; }
        public ICollection<Penalty>? Penalties { get; set; }
    }
}
