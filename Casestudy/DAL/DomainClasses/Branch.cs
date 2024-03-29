﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Casestudy.DAL.DomainClasses
{
    public class Branch
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(150)]
        public string? Street { get; set; }

        [StringLength(150)]
        public string? City { get; set; }

        [StringLength(2)]
        public string? Region { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
        // distance to hold output from sproc
        public double? Distance { get; set; }
    }
}

