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

        // The endpoint for the venues
        const string AvailabilityEndPoint = "/availability";

        const string VenueEndPoint = "/reservations";

        // Private property for the HttpClient object
        private readonly HttpClient _httpClient;

        JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

        // Constructor that accepts an HttpClient instance via dependency injection
        public AvailabilityService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Initialize the HttpClient property
        }

        // Asynchronous method to reserve a venue
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

        public async Task<ReservationPostDTO> ReserveVenue(string venueCode, DateTime eventDate)
        {
            var url = ServiceBaseUrl + VenueEndPoint + "/" + venueCode;
            var reserve = new ReservationGetDTO
            {
                VenueCode = venueCode,
                EventDate = eventDate,
                StaffId = "1",
                Reference = "1"
            };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, reserve);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var confirm = JsonSerializer.Deserialize<ReservationPostDTO>(jsonResponse, jsonOptions);
            if (confirm == null)
            {
                throw new ArgumentNullException(nameof(response), "The Reserve Venue response is null.");
            }
            return confirm;
        }
    }
}

//        // Asynchronous method to free a previously reserved venue
//        public async Task<bool> FreeVenueAsync(string eventId)
//        {
//            // Sending a DELETE request to the Venue EndPoint to free a venue
//            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + VenueEndPoint + $"/free/{eventId}");

//            // Ensuring the response is successful (status code 200-299)
//            response.EnsureSuccessStatusCode();

//            return response.IsSuccessStatusCode;
//        }

//        // Asynchronous method to get available venues based on event suitability
//        public async Task<List<AvailabilityDTO>> GetAvailableVenuesAsync(string eventId)
//        {
//            // Sending a GET request to the Venue EndPoint to get available venues
//            var response = await _httpClient.GetAsync(ServiceBaseUrl + VenueEndPoint + $"/availability/{eventId}");

//            // Ensuring the response is successful (status code 200-299)
//            response.EnsureSuccessStatusCode();

//            // Reading the response as a string
//            var jsonResponse = await response.Content.ReadAsStringAsync();

//            // Deserializing the JSON response to a list of VenueDTO objects
//            var venues = JsonSerializer.Deserialize<List<AvailabilityDTO>>(jsonResponse, jsonOptions);

//            if (venues == null) // Checking if the response is null
//            {
//                throw new ArgumentNullException(nameof(response), "The Venue response is null");
//            }
//            return venues; // Returning the list of available venues
//        }
//    }
//}