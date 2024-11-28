using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ThAmCo.Events.Dtos;

namespace ThAmCo.Events.Services
{
    public class ReservationService
    {
        const string ServiceBaseUrl = "https://localhost:7088/api";

        const string ReservationEndPoint = "/reservations";

        private readonly HttpClient _httpClient;

        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<ReservationGetDTO>> GetVenueAvailabilityListAsync(DateTime date, string eventTypeId)
        {
            var url = ServiceBaseUrl + ReservationEndPoint + "?date=" + date.ToString("yyyy-MM-dd") + "&eventTypeId=" + eventTypeId;

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<ReservationGetDTO>>(jsonResponse, options);

            if (items == null)
            {
                throw new ArgumentNullException(nameof(response), "The Venue Availability list response is null");
            }
            return items;
        }
    }
}
