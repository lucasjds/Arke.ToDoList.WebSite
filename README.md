# Introduction 
The **Arke ToDoList WebSite** Challenge is designed to run via browser. The user can add a new task, edit the name of the task, list of the tasks and change the status of the task.

# Getting Started
Before starting, you require to have installed:
1. Microsoft .NET 8.0
2. Visual Studio (was used Visual Studio 2022 Community)
3. A Browser

# Debug locally
For debugging the application locally, run the respective project directly in your IDE.

# The Architecture
All the train of thought can be followed through the commits in the github. I decided to use Blazor WebAssembly since this would be an evolution of ASP NET MVC. 
Initially, I started doing the component - Home - responsible adding, editing and deleting tasks. Then, I started doing the service that is responsible for calling the API.

After creating the component Home and the Service; I started refactoring the component Home dividing it into 2 other components, one for listing the tasks and other one to creating the tasks (The Form).

# Things to Improve
1. I could have used SnackBar to improve the way we should messages to the user.
2. I could have created pagination for the list
3. I could have created a better design for my webpage.
   


