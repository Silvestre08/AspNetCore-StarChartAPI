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


        public int? OrbitedObjectId { get; }

        [NotMapped]
        public List<CelestialObject> Satellites { get; }

        public TimeSpan OrbitalPeriod { get; }
    }
}
