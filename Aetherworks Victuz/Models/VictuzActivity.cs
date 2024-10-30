using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace Aetherworks_Victuz.Models
{
    public class VictuzActivity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int HostId { get; set; }
        public User? Host { get; set; }
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public decimal MemberPrice { get; set; }
        public DateTime ActivityTime { get; set; }
        public ICollection<UserActivity>? ParticipantsList { get; set; }
        public int ParticipantLimit { get; set; }
        public ActivityCategories Categories { get; set; }

        public enum ActivityCategories
        {
            Free,
            MemFree,
            PayAll,
            MemOnlyFree,
            MemOnlyPay
        }
    }
}
