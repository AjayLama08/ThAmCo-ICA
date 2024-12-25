using Microsoft.DotNet.MSIdentity.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ThAmCo.Events;
using ThAmCo.Events.Dtos;

namespace ThAmCo.Events.Services
{
    public class AvailabilityService
    {
        // The base URL for the venues api service
        const string ServiceBaseUrl = "https://localhost:7088/api";

        // The endpoint for the available venues
        const string AvailabilityEndPoint = "/availability";

        // The endpoint for the reservations
        const string VenueEndPoint = "/reservations";

        // The endpoint for the venue details
        const string VenueDetailsEndPoint = "/venues";

        // Private property for the HttpClient object
        private readonly HttpClient _httpClient;

        JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

        // Constructor that accepts an HttpClient instance via dependency injection
        public AvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Initialize the HttpClient property
        }

        // Asynchronous method to get a list of available venues
        public async Task<List<AvailabilityDTO>> GetAvailabilityListAsync(DateTime beginDate, DateTime endDate, string eventType)
        {
            var requestUrl = $"{ServiceBaseUrl}{AvailabilityEndPoint}?beginDate={beginDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&eventType={eventType}";

            var response = await _httpClient.GetAsync(requestUrl);

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<AvailabilityDTO>>(jsonResponse, jsonOptions);

            if (items == null) // Checking if the response is null
            {
                throw new ArgumentNullException(nameof(response), "The Availability response is null");
            }
            return items; // Returning the list of Availability items
        }

        // Asynchronous method to get a list of available venues
        public async Task<List<AvailabilityDTO>> GetAvailableVenues(DateTime beginDate, DateTime endDate, string eventType)
            { 
            // Construct the request URL based on the provided parameters
            var requestUrl = $"{ServiceBaseUrl}{AvailabilityEndPoint}?beginDate={beginDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}&eventType={eventType}";

            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
             return JsonSerializer.Deserialize<List<AvailabilityDTO>>(content);
            }
        }
}


