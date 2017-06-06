using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class GroupPost
    {
        [Key]
        public int Id { get; set; }
        // Пост
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Длина поста должна быть от 1 до 300 символов")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Пост")]
        public string Post { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
        public virtual Group Group { get; set; }
    }
}