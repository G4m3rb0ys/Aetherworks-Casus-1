using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Aetherworks_Victuz.Models
{
    public class VictuzActivityViewModel
    {
        public VictuzActivity? VictuzActivity { get; set; }
        public ICollection<Participation>? Attendees { get; set; }
        public string? OldPicture { get; set; }

        public void SetOldPicture()
        {
            OldPicture = VictuzActivity.Picture;
        }
    }
}
