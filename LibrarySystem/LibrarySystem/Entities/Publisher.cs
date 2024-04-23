﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Entities
{
    [Table("Publishers")]
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(50)]
        public int EstablishedYear { get; set; }
    }
}