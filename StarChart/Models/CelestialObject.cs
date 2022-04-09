using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarChart.Models
{
    public class CelestialObject
    {
        public int Id { get; }

        [Required]
        public string Name { get; }


        public int? OrbitedObjectId { get; set; }

        [NotMapped]
        public List<CelestialObject> Satellites { get; set; }

        public TimeSpan OrbitalPeriod { get; }
    }
}
