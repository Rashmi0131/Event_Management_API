using EventManagementAPICRUD.Models;

namespace EventManagementAPICRUD.Reporsitory
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<bool> CreateEventAsync(Event objEvent);
        Task<bool> UpdateEventAsync(int id ,Event objEvent);
        Task<Event> DeleteEventAsync(int id);
        Task<List<Event>> SearchEventAsync(SearchEvent searchEvent);
    }
}
