using System.Net.Http.Json;
using System.Text.Json;
using ThAmCo.Events;

namespace ThAmCo.Events.Services
{
    public class VenueService
    {
        // The base URL for the venues api service
        const string ServiceBaseUrl = "https://localhost:7088/api";

        // The endpoint for the venues
        const string VenueEndPoint = "/availability";

        // Private property for the HttpClient object
        private readonly HttpClient _httpClient;

        // Constructor that accepts an HttpClient instance via dependency injection
        public VenueService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Initialize the HttpClient property
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Asynchronous method to reserve a venue
        public async Task<bool> ReserveVenueAsync(string eventId, string venueId)
        {
            var request = new
            {
                EventId = eventId,
                VenueId = venueId
            };

            // Sending a POST request to the Venue EndPoint to reserve a venue
            var response = await _httpClient.PostAsJsonAsync(ServiceBaseUrl + VenueEndPoint + "/reservation", request);

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        // Asynchronous method to free a previously reserved venue
        public async Task<bool> FreeVenueAsync(string eventId)
        {
            // Sending a DELETE request to the Venue EndPoint to free a venue
            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + VenueEndPoint + $"/free/{eventId}");

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        // Asynchronous method to get available venues based on event suitability
        public async Task<List<VenueDTO>> GetAvailableVenuesAsync(string eventId)
        {
            // Sending a GET request to the Venue EndPoint to get available venues
            var response = await _httpClient.GetAsync(ServiceBaseUrl + VenueEndPoint + $"/availability/{eventId}");

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            // Reading the response as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializing the JSON response to a list of VenueDTO objects
            var venues = JsonSerializer.Deserialize<List<VenueDTO>>(jsonResponse, jsonOptions);

            if (venues == null) // Checking if the response is null
            {
                throw new ArgumentNullException(nameof(response), "The Venue response is null");
            }
            return venues; // Returning the list of available venues
        }
    }
}