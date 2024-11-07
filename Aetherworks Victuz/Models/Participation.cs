using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Aetherworks_Victuz.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int ActivityId { get; set; }
        public VictuzActivity? Activity { get; set; }
        public bool Attended { get; set; }
    }
}
