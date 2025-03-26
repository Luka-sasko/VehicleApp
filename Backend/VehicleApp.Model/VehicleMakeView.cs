using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleApp.Model
{
    public class VehicleMakeView
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();
    }
}
