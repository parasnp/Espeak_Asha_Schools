using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IspeakWeb.Models
{
    public class ImgReader
    {
        public int Id { get; set; }

        public required string email { get; set; }

        [NotMapped]

        public IFormFile? Image { get; set; }

        [Display(Name = "Image Url")]
        public string? ImgUrl { get; set; }

        [Display(Name = "Text To Read")]
        public required string Text { get; set; }

    }
}