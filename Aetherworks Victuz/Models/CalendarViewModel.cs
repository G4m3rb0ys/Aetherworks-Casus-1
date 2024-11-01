// Models/CalendarViewModel.cs
using System;
using System.Collections.Generic;

namespace Aetherworks_Victuz.Models
{
    public class CalendarViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<VictuzActivity> Activities { get; set; }
    }
}
