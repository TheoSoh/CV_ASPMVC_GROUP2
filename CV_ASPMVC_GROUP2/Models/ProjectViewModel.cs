﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_ASPMVC_GROUP2.Models
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv en titel.")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vänligen skriv en beskrivning av projektet.")]
        public string Description{ get; set; }

      

        [NotMapped]
        [Required(ErrorMessage = "Du måste lägga in en bild till projektet.")]
        public IFormFile? ImageFile {  get; set; } 
    }
}
