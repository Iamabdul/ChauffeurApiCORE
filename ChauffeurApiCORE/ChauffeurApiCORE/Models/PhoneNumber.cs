using System.ComponentModel.DataAnnotations;

namespace ChauffeurApiCORE.Models
{
    public class PhoneNumber
    {
		[Key]
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
    }
}