using System;
using System.Collections.Generic;
using System.Text;

namespace PapillonProject.Models
{
    public class Observation
    {
        public string IdData { get; set; }
        public string User { get; set; }
        public string TypePapillon { get; set; }
        public string Compte { get; set; }
        public string LatLng { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Timestamp { get; set; }
        public TypePapillon Papillon { get; set; }
        public DateTime? Date { get; set; }
    }
}
