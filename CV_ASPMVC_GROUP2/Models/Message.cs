﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CV_ASPMVC_GROUP2.Models
{
    [Serializable]
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime SentTime { get; set; } = DateTime.Now;
        public bool? Read { get; set; }
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }

        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Vänligen ange endast bokstäver.")]
        public string? FromAnonymousName { get; set; }

        [ForeignKey(nameof(FromUserId))]
        [XmlIgnore]
        public virtual User? FromUser { get; set; }

        [ForeignKey(nameof(ToUserId))]
        [XmlIgnore]
        public virtual User? ToUser { get; set; }
        
    }
}
