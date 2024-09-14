using static Arke.ToDoList.WebSite.Pages.Home;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System;
using System.Text;

namespace Arke.ToDoList.WebSite.Services;

public class TaskService : ITaskService
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
        }
        catch (Exception ex)
        {
            throw;
        }

        return null;
    }

    public async Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        StringContent content = new StringContent(JsonSerializer.Serialize(task),
                                                 Encoding.UTF8, "application/json");
        using var response = await _httpClient.PostAsync("Task", content);
        if (response.IsSuccessStatusCode)
        {
            var taskItem = await response.Content.ReadFromJsonAsync<TaskItem>();
            return taskItem;
        }
        return null;
    }
}