namespace jwtask.Controllers
{
    public class Customiddleware
    {
        private readonly RequestDelegate _next;

        public Customiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}  before starting");
            await _next(context);
            Console.WriteLine($"Response: {context.Response.StatusCode} finised");
        }
    }
}
