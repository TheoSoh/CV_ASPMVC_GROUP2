﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class Address
    {
        public int Id { get; set; }

        [RegularExpression(@"^[\p{L}\d]*$", ErrorMessage = "Vänligen ange endast bokstäver och siffror.")]
        public string? Street { get; set; }

        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string? City { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Postnumret kan endast innehålla siffror.")]
        public int? PostalCode { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}
