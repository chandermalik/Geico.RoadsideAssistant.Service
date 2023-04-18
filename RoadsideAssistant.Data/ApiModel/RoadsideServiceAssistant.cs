using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace RoadsideAssistant.Data.Entities.ApiModel
{
    public class RoadsideServiceAssistant
    {
        [Required(ErrorMessage = "Missing Business Name")]
        public string BusinessName { get; set; }
        public string StreetAddress { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "Missing Zip Code")]
        [StringLength(5,ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }

        public string State { get; set; }
        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public GeoLocation? CurrentLocation { get; set; }
        public bool IsAssigned { get; set; }

        public Customer? CustomerAssigned { get; set; }
    }

    [XmlRoot("RoadsideServiceAssistants")]
    public class RoadsideServiceAssistants
    {
        public RoadsideServiceAssistants()
        {
            RoadsideServiceAssistantsList = new List<RoadsideServiceAssistant>();
        }

        [XmlElement("RoadsideServiceAssistant")]
        public List<RoadsideServiceAssistant> RoadsideServiceAssistantsList { get; set; }
    }
}