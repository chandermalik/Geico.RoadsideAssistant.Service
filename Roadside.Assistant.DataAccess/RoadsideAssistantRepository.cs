using RoadsideAssistant.Data.Entities.ApiModel;
using System.Xml.Serialization;
using System.Xml;

namespace RoadsideAssistant.DataAccess
{
    public class RoadsideAssistantRepository : IRoadsideAssistantRepository
    {
        // This class acts as data repository class which will interact with database and get or update the data
        // the logic and action taken are for mock purposes. In actual, real data will get update
        public IEnumerable<RoadsideServiceAssistant> FindNearestAssistants(GeoLocation geoLocation, int limit)
        {
            // This method in actual will have logic to fetch all the nearest road side service assistants near to geolocation
            // In actual this method will calculate the distance of each assistance from given geolocation and sort on distance
            // For assignment demonstration it is using a existing xml file.

            var serviceAssistants = GetRoadsideServiceAssistants();

            // Logic to calculate distance from given zip code or real location of customer
            // it will use given geolocation coordinates data from table.

            return serviceAssistants; // return the sorted list.
        }

        public void UpdateAssistantLocation(RoadsideServiceAssistant assistant, GeoLocation assistantLocation)
        {

            var roadsideServiceAssistants = GetRoadsideServiceAssistants()?.ToList();

            if (roadsideServiceAssistants == null)
                throw new Exception("Not Found");

            // this will find the road side assistant to update location. if found , will update the location.
            foreach (var sa in roadsideServiceAssistants.Where(sa => sa.BusinessName == assistant.BusinessName && sa.Zip == assistant.Zip))
            {
                sa.CurrentLocation = assistantLocation;
                return;

            }

            // Here update the service assistant location in the database.

            throw new Exception("Not Found");
        }

        public RoadsideServiceAssistant ReserveAssistant(Customer? customer, RoadsideServiceAssistant assistant)
        {
            var roadsideServiceAssistants = GetRoadsideServiceAssistants()?.ToList();

            if (roadsideServiceAssistants == null)
                throw new Exception("Not Found");

            // this will find the road side assistant to reserve. if found , will reserve it and assign the customer.
            foreach (var sa in roadsideServiceAssistants.Where(sa => sa.BusinessName == assistant.BusinessName && sa.Zip == assistant.Zip))
            {
                sa.IsAssigned = true;
                sa.CustomerAssigned = customer;
                return sa;
            }

            // Here update the service assistant reservation to the customer in the database in appropriate table maintaining reservation.

            throw new Exception("Not Found");
        }

        public void ReleaseAssistant(Customer? customer, RoadsideServiceAssistant assistant)
        {
            var roadsideServiceAssistants = GetRoadsideServiceAssistants()?.ToList();

            if (roadsideServiceAssistants == null)
                throw new Exception("Not Found");

            // this will find the road side assistant to release. if found , will remove any customer assignment.
            foreach (var sa in roadsideServiceAssistants.Where(sa => sa.BusinessName == assistant.BusinessName && sa.Zip == assistant.Zip))
            {
                sa.IsAssigned = false;
                sa.CustomerAssigned = null;
                return;
            }

            // Here remove the service assistant reservation to the customer in the database in appropriate table maintaining reservation.
            // Service assistant 'Is Assigned' flag should be false in DB

            throw new Exception("Not Found");

        }

        public IEnumerable<RoadsideServiceAssistant> GetRoadsideServiceAssistants()
        {
            var xmlPath = "DataSource\\RoadsideAssistantsData.xml";
            var ser = new XmlSerializer(typeof(RoadsideServiceAssistants));

            using var reader = XmlReader.Create(xmlPath);
            var serviceAssistants = (RoadsideServiceAssistants)ser.Deserialize(reader)!;

            return serviceAssistants.RoadsideServiceAssistantsList;
        }
    }
}