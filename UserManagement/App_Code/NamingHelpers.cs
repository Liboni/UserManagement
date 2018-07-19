
namespace UserManagement
{
    using Microsoft.AspNetCore.Mvc;

    public class NamingHelpers
    {
        public static string GetOperationId<T>(string actionName) where T : Controller => $"{GetControllerName<T>()}{actionName}";

        public static string GetControllerName<T>() where T : Controller => typeof(T).Name.Replace(nameof(Controller), string.Empty);

    }
}
