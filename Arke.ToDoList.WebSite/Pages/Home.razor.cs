using Arke.ToDoList.WebSite.Services;
using Arke.ToDoList.WebSite.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Arke.ToDoList.WebSite.Pages;

public class HomeModel : ComponentBase
{
    [Inject] private ITaskService TaskService { get; set; }
    [Inject] IJSRuntime JS { get; set; }
    protected List<TaskItem> tasks = new();
    protected TaskItem currentTask = new();
    protected TaskItem taskToEdit = null;
    protected bool isEditing = false;
    protected HashSet<TaskItem> statusSelectorVisible = new HashSet<TaskItem>();

    protected override async Task OnInitializedAsync()
    {
        var list = await Task.Run(() => TaskService.GetTasksAsync());
        tasks = list.ToList();
    }

    protected async Task HandleSubmit()
    {
        try
        {
            if (isEditing)
            {
                await UpdateTask();
            }
            else
            {
                await AddTask();
            }
        }
        catch (Exception ex)
        {
            await JS.InvokeVoidAsync("alert", ex.Message);
        }
        finally
        {
            ResetForm();
        }

    }

    private async Task UpdateTask()
    {
        // Update existing task
        if (taskToEdit != null)
        {
            taskToEdit.Name = currentTask.Name;
            taskToEdit.Description = currentTask.Description;
            taskToEdit.Status = currentTask.Status;
            await Task.Run(() => TaskService.UpdateTaskAsync(currentTask.Id, taskToEdit));
            await JS.InvokeVoidAsync("alert", "Task updated successfully!");
            // Reset the reference
            taskToEdit = null;
        }
    }

    private async Task AddTask()
    {
        // Add new task
        var newTask = new TaskItem
        {
            Name = currentTask.Name,
            Description = currentTask.Description,
            Status = currentTask.Status
        };
        var result = await Task.Run(() => TaskService.AddTaskAsync(newTask));
        await JS.InvokeVoidAsync("alert", "Task added successfully!");
        tasks.Add(result);
    }

    protected void StartEditTask(TaskItem task)
    {
        statusSelectorVisible = new HashSet<TaskItem>();
        currentTask = new TaskItem
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status
        };
        taskToEdit = task; // Keep reference to the task to edit
        isEditing = true;
    }

    protected async Task DeleteCompletedTasks()
    {
        if (tasks.Any(x => x.Status == "Done"))
        {
            try
            {
                await Task.Run(() => TaskService.DeleteCompletedTasks());
                await JS.InvokeVoidAsync("alert", "everything deleted!");
                tasks.RemoveAll(x => x.Status == "Done");
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }

    protected void ResetForm()
    {
        currentTask = new TaskItem();
        isEditing = false;
        taskToEdit = null;
    }
}
