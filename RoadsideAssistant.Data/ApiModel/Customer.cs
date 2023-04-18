
namespace RoadsideAssistant.Data.Entities.ApiModel
{
    public class Customer
    {
        public string MembershipId { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerRoadSideServiceAssistance
    {
        public Customer Customer { get; set; }
        public GeoLocation Location { get; set; }

        public string RoadsideServiceAssistantName { get; set; }

        public string Zip { get; set; }
    }
}
