// Models/CompositeViewModel.cs
using System.Collections.Generic;

namespace Aetherworks_Victuz.Models
{
    public class CompositeViewModel
    {
        public CalendarViewModel Calendar { get; set; }
        public IEnumerable<VictuzActivity> Activities { get; set; }
    }
}
