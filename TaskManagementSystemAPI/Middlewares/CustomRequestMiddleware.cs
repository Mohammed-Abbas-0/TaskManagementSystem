namespace TaskManagementSystemAPI.Middlewares
{
    public class CustomRequestMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomRequestMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // قراءة معلومات الـ Request
            var method = context.Request.Method;  // GET, POST, PUT
            var path = context.Request.Path;      // /api/your-endpoint
            var queryParams = context.Request.Query;  // { "id": "123", "name": "John" }
            var headers = context.Request.Headers; // مثل "Authorization", "Content-Type", إلخ

            // طباعة هذه البيانات في الكونسول
            Console.WriteLine($"Method: {method}");
            Console.WriteLine($"Path: {path}");
            Console.WriteLine("Query Parameters:");
            foreach (var param in queryParams)
            {
                Console.WriteLine($"{param.Key}: {param.Value}");
            }

            Console.WriteLine("Headers:");
            foreach (var header in headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            // إرسال التحكم للـ Middleware التالي في الـ pipeline
            await _next(context);
        }
        
    }
}
