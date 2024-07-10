using static System.Net.Mime.MediaTypeNames;

namespace EmployeeAttendenceSystem.Middleware
{
    public class GlobalExceptionHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = Text.Plain;

            await response.WriteAsync("OOPS! THERE IS AN EXCEPTION \n" + ex.StackTrace + ex.InnerException + ex.Message);
        }
    }
}
