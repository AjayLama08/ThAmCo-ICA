using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using ThAmCo.Events.Dtos;

namespace ThAmCo.Events.Services
{
    public class EventTypeService
    {
        const string ServiceBaseUrl = "http://localhost:7011/api/api";

        const string EventTypeEndPoint = "/EventTypes";

        private readonly HttpClient _httpClient;

        public EventTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<EventTypeDTO>> GetEventItemsAsync()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + EventTypeEndPoint);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var items = JsonSerializer.Deserialize<List<EventTypeDTO>>(jsonResponse, jsonOptions);

            if (items == null)
            {
               throw new ArgumentNullException(nameof(response), "The EventType response is null");
            }
            return items;
        }

        public async Task<List<SelectListItem>> GetEventTypeSelectionListAsync()
        {
            var eventTypes = await GetEventItemsAsync();

            var selectList = new List<SelectListItem>();

            if (eventTypes != null)
            {
                selectList = eventTypes.Select(e => new SelectListItem
                {
                    Value = e.EventTypeId,
                    Text = e.EventName
                }).ToList();
            }
            return selectList;
        }
    }
}
