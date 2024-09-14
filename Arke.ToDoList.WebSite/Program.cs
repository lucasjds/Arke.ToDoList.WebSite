using Arke.ToDoList.WebSite;
using Arke.ToDoList.WebSite.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("HttpClientAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:6500/");
});

builder.Services.AddScoped<ITaskService, TaskService>();

await builder.Build().RunAsync();

