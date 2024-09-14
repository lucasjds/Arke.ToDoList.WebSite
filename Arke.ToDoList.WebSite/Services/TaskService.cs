using static Arke.ToDoList.WebSite.Pages.Home;
using System.Net.Http.Json;
using System.Net.Http;

namespace Arke.ToDoList.WebSite.Services;

public class TaskService
{
    private readonly HttpClient _httpClient;
    public TaskService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HttpClientAPI");
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("Task");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<TaskItem>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
        }

        return null;
    }
}
