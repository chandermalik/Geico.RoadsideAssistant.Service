# Geico.RoadsideAssistant.Service

Use following link for this API documentation and schema. Change local host and port as per machine where it is ran.

http://localhost:5280/swagger/index.html

---------------------------------------------------------------------------
Data Store -> Data store in this case is RoadsideAssistantsData.xml

---------------------------------------------------------------------------
This API has one controller named RoadsideAssistantController.
Controller has 4 end points:
	
	Get - > It finds and gets 5 nearest road side service assistants. It takes 3 parameters.
		    Longitude, Latitude and Limit.
    
	Route URL -> http://localhost:5280/api/RoadsideAssistant/345.55/234.55/5

 Response-

 [
    {
        "businessName": "ABC Roadside Services",
        "streetAddress": "23 Main St.",
        "city": "NewYork",
        "zip": "11001",
        "state": "NY",
        "contactPhone": "234-456-8765",
        "contactEmail": "abcroadside@gmail.com",
        "currentLocation": null,
        "isAssigned": false,
        "customerAssigned": null
    },
    {
        "businessName": "XYZ Roadside Services",
        "streetAddress": "27 Main St.",
        "city": "Bethesda",
        "zip": "20846",
        "state": "MD",
        "contactPhone": "154-456-8765",
        "contactEmail": "xyzroadside@gmail.com",
        "currentLocation": null,
        "isAssigned": false,
        "customerAssigned": null
    },
    {
        "businessName": "Car Roadside Services",
        "streetAddress": "35 South St.",
        "city": "Rockville",
        "zip": "20814",
        "state": "MD",
        "contactPhone": "564-456-8765",
        "contactEmail": "carroadside@gmail.com",
        "currentLocation": null,
        "isAssigned": false,
        "customerAssigned": null
    },
    {
        "businessName": "Hello Roadside Services",
        "streetAddress": "12 Walker St.",
        "city": "Wayne",
        "zip": "19087",
        "state": "PA",
        "contactPhone": "484-456-8765",
        "contactEmail": "helloroadside@gmail.com",
        "currentLocation": null,
        "isAssigned": false,
        "customerAssigned": null
    },
    {
        "businessName": "Newstar Roadside Services",
        "streetAddress": "65 Rockville Pike",
        "city": "Rockville",
        "zip": "20814",
        "state": "NY",
        "contactPhone": "240-456-8765",
        "contactEmail": "newstarroadside@gmail.com",
        "currentLocation": null,
        "isAssigned": false,
        "customerAssigned": null
    }
]

------------------------------------------------------------------------------

Update Roadside Assistant Location

	Post Method -> UpdateAssistantLocation. Following is request example..

	{
    "BusinessName": "Hello Roadside Services",
    "ZipCode": "19087",
    "Location": {
        "Latitude": 156.450,
        "Longitude": 127.084
      }
    }

    It will send Name and zip code of service assistant to update the location. In real world, it will have some unique ID of
    service assistant. For this assignment, business name and zip code is used to fetch from data store.
    System will fetch the service assistant from Data Store and if found, will update the location. If not found then 
    Not Found (404) is returned. Data store in this case is RoadsideAssistantsData.xml

   Route URL -> http://localhost:5280/api/RoadsideAssistant/Update

------------------------------------------------------------------------------------

Reserve Roadside Assistant to a customer

	Post Method -> ReserveAssistant. Following is request example.

    {
    "Customer": {
        "MembershipId": "2334444",
        "AccountNumber": "798439944",
        "FirstName": "Chander",
        "LastName": "Malik"
    },
    "Location": {
        "Latitude": 110.044,
        "Longitude": 174.059
    },
    "RoadsideServiceAssistantName": "XYZ Roadside Services",
    "Zip": "20846"
}

    It will send Customer object, its location (not requierd though), name and zip code of service assistant to to reserve.
    System will search and fetch the service assistant from Data Store and if found, it will get assigned to the customer. If not found then 
    Not Found (404) is returned. Updated roadside service assistant will be sent back in response.

   Route URL -> http://localhost:5280/api/RoadsideAssistant/Reserve

----------------------------------------------------------------------------------------

Release Roadside Assistant from a customer

	Post Method -> ReleaseAssistant. Following is request example.

{
    "Customer": {
        "MembershipId": "2334444",
        "AccountNumber": "798439944",
        "FirstName": "Chander",
        "LastName": "Malik"
    },
    "Location": {
        "Latitude": 110.044,
        "Longitude": 174.059
    },
    "RoadsideServiceAssistantName": "XYZ Roadside Services",
    "Zip": "20846"
}

    It will send Customer object, its location, name and zip code of service assistant to release from customer.
    System will search and fetch the service assistant from Data Store and if found, it will release the customer. If not found then 
    Not Found (404) is returned.

   Route URL -> http://localhost:5280/api/RoadsideAssistant/Release

------------------------------------------------------------------------------------------------------------

Design Considerations

- The solution has 3 projects.
    API -> Web API
   
   RoadsideAssistantManager -> It is business logic layer to do any business logic before call is made to data store and to perform any business
                                logic on the data retrieved from the Data store.
   
   RoadsideAssistant.DataAccess -> This project is data access repository layer to fetch and update from and to data store. For this assignment, data is 
                                   stored in a xml file named RoadsideAssistantsData.xml

   RoadsideAssistant.Data.Entities -> This project is for having Data model , entities and API request. In real world there can be multiple entity project
                                      for DB entities, business objects and API model. For this assignment all are placed in same project.

                                      Following are classes/schema

                                      1. RoadsideServiceAssistant -> This entity has the details about roadside service assistant.
                                      2. Customer -> This entity has customer information.
                                      3. GeoLocation -> This entity has longitude and latitude for the location.
                                      4. UpdateAssistantRequest -> This is request model to update the service assistant location.
                                      5. CustomerRoadSideServiceAssistance -> This is request model to reserve or release the service assistant.

- The project structure and layers are created based on best of knowledge as given in assignment. In real requirements this can be different.

- Logging is not implemented but ILogger can be used to log the request, response and exceptions into table, file or on cloud.

- Swagger is added to view the API document and schema. Use above request when try http://localhost:5280/swagger/index.html

