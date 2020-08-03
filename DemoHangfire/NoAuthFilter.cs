using Hangfire.Dashboard;
using Hangfire.Annotations;

namespace DemoHangfire
{
    public class NoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
