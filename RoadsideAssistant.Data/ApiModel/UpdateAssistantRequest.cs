using System.ComponentModel.DataAnnotations;

namespace RoadsideAssistant.Data.Entities.ApiModel
{
    public class UpdateAssistantRequest
    {
        [Required(ErrorMessage = "Missing Business Name")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Missing Zip Code")]
        [StringLength(5, ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }

        public GeoLocation Location { get; set; }

        //public string Longitude { get; set; }

        //public string Latitude { get; set; }
    }
}
