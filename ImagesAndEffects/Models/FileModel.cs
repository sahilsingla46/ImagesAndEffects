using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ImagesAndEffects.Models
{
    public class FileModel
    {
        [FileExtensions(Extensions = "jpg,jpeg")]
        [Required(ErrorMessage = "Please Enter FileName.")]
        public string fileName { get; set; }


        [Required(ErrorMessage = "Please Enter File.")]
        public string filePath { get; set; }


        [Required(ErrorMessage = "Please Enter Size.")]
        [Range(100, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1} pixels.")]
        public int size { get; set; }

        public List<string> Effects { get; set; }
    }
}
