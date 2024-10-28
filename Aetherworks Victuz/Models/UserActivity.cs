using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Aetherworks_Victuz.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IdentityUser? User { get; set; }
        public int ActivityId { get; set; }
        public VictuzActivity? Activity { get; set; }
    }
}
