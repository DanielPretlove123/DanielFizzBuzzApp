using System.Reflection;

namespace InfoTrackFizzBuzz.Web.Infrastructure;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithGroupName(groupName)
            .WithTags(groupName);
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !a.FullName!.StartsWith("Microsoft") && !a.FullName.StartsWith("System"));

        foreach (var assembly in assemblies)
        {
            try
            {
                var endpointGroupTypes = assembly.GetExportedTypes()
                    .Where(t => t.IsSubclassOf(endpointGroupType) && !t.IsAbstract);

                foreach (var type in endpointGroupTypes)
                {
                    if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                    {
                        instance.Map(app);
                    }
                }
            }
            catch (Exception)
            {
                continue;
            }
        }

        return app;
    }
}
