using RoadsideAssistant.Data.Entities.ApiModel;

namespace RoadsideAssistant.Manager
{
    public interface IRoadsideAssistantManager
    {
        IEnumerable<RoadsideServiceAssistant> FindNearestAssistants(GeoLocation geoLocation, int limit);

        public IEnumerable<RoadsideServiceAssistant> GetRoadsideServiceAssistants();

        public RoadsideServiceAssistant? GetRoadsideServiceAssistant(string serviceAssistantBusinessName, string zipCode);

        void UpdateAssistantLocation(RoadsideServiceAssistant assistant, GeoLocation assistantLocation);

        RoadsideServiceAssistant? ReserveAssistant(Customer? geicoCustomer, RoadsideServiceAssistant assistant);

        void ReleaseAssistant(Customer? customer, RoadsideServiceAssistant assistant);


    }
}