﻿using static Arke.ToDoList.WebSite.Pages.Home;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System;
using System.Text;
using System.Threading.Tasks;

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
        using var response = await _httpClient.PostAsJsonAsync("Task", task);
        if (response.IsSuccessStatusCode)
        {
            var taskItem = await response.Content.ReadFromJsonAsync<TaskItem>();
            return taskItem;
        }
        var message = await response.Content.ReadAsStringAsync();
        throw new Exception($"Status Code : {response.StatusCode} - {message}");
    }

    public async Task<TaskItem> UpdateTaskAsync(string id, TaskItem task)
    {
        using var response = await _httpClient.PutAsJsonAsync($"Task/{id}", task);
        if (response.IsSuccessStatusCode)
        {
            var taskItem = await response.Content.ReadFromJsonAsync<TaskItem>();
            return taskItem;
        }
        var message = await response.Content.ReadAsStringAsync();
        throw new Exception($"Status Code : {response.StatusCode} - {message}");
    }

    public Task<bool> ChangeStatus(string id, bool changeStatus)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCompletedTasks()
    {
        using var result = await _httpClient.DeleteAsync($"Task/completed-tasks");
        if (result.IsSuccessStatusCode)
        {
            return true;
        }
        var message = await result.Content.ReadAsStringAsync();
        throw new Exception($"Status Code : {result.StatusCode} - {message}");
    }
}