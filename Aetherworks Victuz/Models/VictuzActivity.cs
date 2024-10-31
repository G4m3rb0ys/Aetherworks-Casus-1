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
        public ActivityCategories Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public DateTime ActivityDate { get; set; }
        public int HostId { get; set; }
        public User? Host { get; set; }
        public decimal? Price { get; set; }
        public decimal? MemberPrice { get; set; }
        public int ParticipantLimit { get; set; }
        public ICollection<Participation>? ParticipantsList { get; set; }

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
