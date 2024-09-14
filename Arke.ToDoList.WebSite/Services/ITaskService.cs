using static Arke.ToDoList.WebSite.Pages.Home;

namespace Arke.ToDoList.WebSite.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync();
    Task<TaskItem> AddTaskAsync(TaskItem task);
    Task<TaskItem> UpdateTaskAsync(string id, TaskItem task);
    Task<bool> ChangeStatus(string id, bool changeStatus);
    Task<bool> DeleteCompletedTasks();
}
