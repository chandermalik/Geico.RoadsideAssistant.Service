using RoadsideAssistant.Data.Entities.ApiModel;
using RoadsideAssistant.DataAccess;

namespace RoadsideAssistant.Manager
{
    public class RoadSideAssistanceManager:IRoadsideAssistantManager
    {
        private readonly IRoadsideAssistantRepository _roadSideAssistanceRepository;

        public RoadSideAssistanceManager(IRoadsideAssistantRepository roadSideAssistanceRepository)
        {
            _roadSideAssistanceRepository = roadSideAssistanceRepository;
        }

        public IEnumerable<RoadsideServiceAssistant> GetRoadsideServiceAssistants()
        {
            return _roadSideAssistanceRepository.GetRoadsideServiceAssistants();
        }

        public RoadsideServiceAssistant? GetRoadsideServiceAssistant(string serviceAssistantBusinessName, string zipCode)
        {
            var serviceAssistantProviders = GetRoadsideServiceAssistants();

            var serviceAssistant = serviceAssistantProviders?.FirstOrDefault(s => s.BusinessName == serviceAssistantBusinessName && s.Zip == zipCode);

            return serviceAssistant;
        }

        public IEnumerable<RoadsideServiceAssistant> FindNearestAssistants(GeoLocation geoLocation, int limit)
        {
            return _roadSideAssistanceRepository.FindNearestAssistants(geoLocation, limit);
        }

        public void UpdateAssistantLocation(RoadsideServiceAssistant assistant, GeoLocation assistantLocation)
        {
            _roadSideAssistanceRepository.UpdateAssistantLocation(assistant, assistantLocation);
        }

        public RoadsideServiceAssistant? ReserveAssistant(Customer? customer, RoadsideServiceAssistant assistant)
        {
            return _roadSideAssistanceRepository.ReserveAssistant(customer, assistant);
        }

        public void ReleaseAssistant(Customer? customer, RoadsideServiceAssistant assistant)
        {
            _roadSideAssistanceRepository.ReleaseAssistant(customer, assistant);

        }
    }
}
