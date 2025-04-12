namespace TaskManagementSystemAPI.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        // Constructor لتحديد الـ RequestDelegate والـ Logger
        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // تسجيل الوقت قبل معالجة الـ Request
            var startTime = DateTime.UtcNow;

            // متابعة الـ Request إلى الـ Middleware التالي
            await _next(context);

            // تسجيل الوقت بعد المعالجة
            var endTime = DateTime.UtcNow;

            // حساب المدة الزمنية
            var duration = endTime - startTime;

            // إضافة المدة الزمنية في الـ Response Headers
         //   context.Response.Headers["X-Request-Duration"] = duration.TotalMilliseconds.ToString() + " ms";

            // طباعة المدة الزمنية في الكونسول
            _logger.LogInformation($"Request to {context.Request.Path} took {duration.TotalMilliseconds} ms");
        }
    }

}
