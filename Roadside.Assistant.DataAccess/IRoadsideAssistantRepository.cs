using RoadsideAssistant.Data.Entities.ApiModel;

namespace RoadsideAssistant.DataAccess
{
    public interface IRoadsideAssistantRepository
    {
        // Find nearest max limit service assistants
        IEnumerable<RoadsideServiceAssistant> FindNearestAssistants(GeoLocation geoLocation, int limit);

        // Get all roadside service assistants
        IEnumerable<RoadsideServiceAssistant> GetRoadsideServiceAssistants();

        void UpdateAssistantLocation(RoadsideServiceAssistant assistant, GeoLocation assistantLocation);

        RoadsideServiceAssistant ReserveAssistant(Customer? geicoCustomer, RoadsideServiceAssistant assistant);

        void ReleaseAssistant(Customer? customer, RoadsideServiceAssistant assistant);
    }
}
