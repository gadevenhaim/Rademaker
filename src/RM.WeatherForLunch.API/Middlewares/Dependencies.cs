using System.Reflection;

namespace RM.WeatherForLunch.Web.Middlewares
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
            foreach (var assembly in assemblies)
            {
                RegisterDependenciesOfAssembly(services, assembly);
            }

            return services;
        }

        private static void RegisterDependenciesOfAssembly(IServiceCollection services, Assembly assembly)
        {
            var exportedTypes = assembly.GetExportedTypes()
                .Where(exportedType => !exportedType.IsInterface && !exportedType.IsAbstract)
                .Select(a => new { assignedType = a, serviceType = a.GetInterfaces().Where(_interface => _interface.Name.Contains(a.Name)).ToList() }).ToList();

            exportedTypes.ForEach(typesToRegister =>
            {
                typesToRegister.serviceType.ForEach(
                    typeToRegister => services.AddScoped(typeToRegister, typesToRegister.assignedType));
            });

        }
    }
}
