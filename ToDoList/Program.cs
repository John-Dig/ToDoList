using Microsoft.AspNetCore.Builder; // this namespace lets us access tools to create and configure a web application host
using Microsoft.Extensions.DependencyInjection; //this namespace allows us to access tools to create services within our web app that get added to our web application via dependency injection

namespace ToDoList
{
  class Program
  {
    static void Main(string[] args)
    {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args); //creates a web host: includes all application's resources and configurations needed to run as a web app in a browser.

      builder.Services.AddControllersWithViews(); //this  customize our host builder by enabling the Model-View-Controller (MVC) framework as a service. The services get added to the builder.Services property by invoking the AddControllersWithViews() method

      WebApplication app = builder.Build(); //this creates and returns our web app host, which we save in the variable app. As we can see, app is of the type WebApplication
      
      app.UseHttpsRedirection();
      
      app.UseRouting(); //this specifies that we want our host to match the website URL to routes that we create within our app. For example, if the URL is localhost:5000/Home/Index, our host will know to match _Home/Index to a route that we coded in our project. This will make more sense once we start defining routes in controller files

      app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
      ); //this establishes a convention for our routes to follow. This convention is the default pattern that our routes will follow. In this case, we are saying that we want our URLs to first list the name of the controller, then the name of the action, and then the id of the current object, if any. Just like before, this will make more sense once we start defining routes in controller files

      app.Run();
    }
  }
}