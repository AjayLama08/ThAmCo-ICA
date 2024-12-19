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

        // Asynchronous method to reserve a venue
        public async Task<ReservationPostDTO> PostReserveVenue(string venueCode, DateTime eventDate)
        {
            string staffId = "1";
            var url = ServiceBaseUrl + VenueEndPoint;
            var reserve = new ThAmCo.Events.Dtos.ReservationPostDTO
            {
                EventDate = eventDate,
                VenueCode = venueCode,
                StaffId = staffId
            };

            try
            {
                //Log the request details
                Console.WriteLine($"Request URL: {url}");
                Console.WriteLine($"Request Body: {JsonSerializer.Serialize(reserve, jsonOptions)}");

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, reserve);

                // Log the response status code
                Console.WriteLine($"Response Status Code: {response.StatusCode}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Log the response body
                Console.WriteLine($"Response Body: {jsonResponse}");

                var confirm = JsonSerializer.Deserialize<ReservationPostDTO>(jsonResponse, jsonOptions);
                if (confirm == null)
                {
                    throw new ArgumentNullException(nameof(response), "The Reserve Venue response is null.");
                }
                return confirm;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // Asynchronous method to free a previously reserved venue
        public async Task<bool> FreeVenueAsync(string reference)
        {
            var url = $"{ServiceBaseUrl}{VenueEndPoint}/{reference}";

            // Sending a DELETE request to the Venue EndPoint to free a venue
            var response = await _httpClient.DeleteAsync(url);

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        // Asynchronous method to get the venue code
        public async Task<string> GetVenueCodeAsync(string reference)
        {
            if (reference == null) // Checking if the reference is null
            {
                return "Not Known";
            }

            var url = $"{ServiceBaseUrl}{VenueEndPoint}/{reference}";

            var response = await _httpClient.GetAsync(url);

            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var reservation = JsonSerializer.Deserialize<ReservationPostDTO>(jsonResponse, jsonOptions);

            if (reservation == null) // Checking if the response is null
            {
                throw new ArgumentNullException(nameof(response), "The Reservation response is null");
            }

            return reservation.VenueCode; // Returning the venue code
        }

        // Asynchronous method to get venue details
        public async Task<VenueDetailsDTO> GetVenueDetailsAsync(string venueCode)
        {
            if (venueCode.Equals("Not Known"))
            {
                return new VenueDetailsDTO
                {
                    VenueCode = "",
                    VenueName = "",
                    Description = "",
                    Capacity = 0,
                };
            }

            var requestUrl = $"{ServiceBaseUrl}{VenueDetailsEndPoint}/{venueCode}";

            var response = await _httpClient.GetAsync(requestUrl);
            // Ensuring the response is successful (status code 200-299)
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var venueDetails = JsonSerializer.Deserialize<VenueDetailsDTO>(jsonResponse, jsonOptions);

            if (venueDetails == null)
            {
                throw new ArgumentNullException(nameof(response), "The Venue Details response is null");
            }

            return venueDetails;
        }
    }
}
