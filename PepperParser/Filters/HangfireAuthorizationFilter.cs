using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace PepperParser.Filters
{
    //Фильтр доступа в пануль управления фоновыми задачами
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("admin");
        }
    }
}