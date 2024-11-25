using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ThAmCo.Events;

namespace ThAmCo.Events.Services
{
    public class EventTypeService
    {
        // The base URL for the api service
        const string ServiceBaseUrl = "https://localhost:7088/api";

        // The endpoint for the event types
        const string EventTypeEndPoint = "/eventtypes";

        // Private peoperty for the HttpClient object
        private readonly HttpClient _httpClient;

        //Constructor that accepts an HttpClient instance via dependency injection
        public EventTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient; //Initialise the HttpClient property
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        //Asynchronus method to fetch the event types from the api
        public async Task<List<EventTypeDTO>> GetEventItemsAsync()
        {
            //Sending a GET request to the EventType EndPoint
            var response = await _httpClient.GetAsync(ServiceBaseUrl + EventTypeEndPoint);

            //Ensuring the response is successful(status code 200-299)
            response.EnsureSuccessStatusCode();

            //Reading the response as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            //Deserializing the JSON response to a list of EventTypeDTO objects
            var items = JsonSerializer.Deserialize<List<EventTypeDTO>>(jsonResponse, jsonOptions);

            if (items == null) //Checking if the response is null
            {
               throw new ArgumentNullException(nameof(response), "The EventType response is null");
            }
            return items; //Returning the list of EventType items
        }

        //Asynchronus method to get the event types as a select list
        public async Task<List<SelectListItem>> GetEventTypeSelectListAsync()
        {
            //Calling the GetEventItemsAsync method to get the event types
            var eventType = await GetEventItemsAsync();

            //Creating a new list of SelectListItem objects
            var selectList = new List<SelectListItem>();

            if (eventType != null)
            {
                selectList = eventType.Select(e => new SelectListItem
                {
                    Value = e.EventTypeId,
                    Text = e.EventTypeId
                }).ToList();
            }
            return selectList;
        }
    }
}
