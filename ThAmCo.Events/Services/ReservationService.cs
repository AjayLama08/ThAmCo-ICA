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

        public async Task<List<ReservationGetDTO>> GetReservationsGetAsync()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + ReservationEndPoint);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var reservations = JsonSerializer.Deserialize<List<ReservationGetDTO>>(jsonResponse, options);

            if (reservations == null)
            {
                throw new ArgumentNullException(nameof(response), "The Reservation response is null");
            }
            return reservations;
        }

        public async Task<List<ReservationPostDTO>> PostReservationPostAsync()
        {
            var reservations = await GetReservationsGetAsync();

            var postDTO = new List<ReservationPostDTO>();

            if (reservations != null)
            {
                foreach (var reservation in reservations)
                {
                    postDTO.Add(new ReservationPostDTO
                    {
                        EventDate = reservation.EventDate,
                        VenueCode = reservation.VenueCode,
                        StaffName = reservation.StaffName
                    });
                }
            }
            return postDTO;
        }
    }
}
