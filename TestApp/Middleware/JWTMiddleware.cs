using System.Text;

namespace TestApp.Middleware
{
	public class JWTMiddleware
	{
		private readonly RequestDelegate _next;

		public JWTMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("JwtToken", out string? jwtToken))
			{

				if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
				{
					context.Request.Headers.Add("Authorization", $"Bearer {jwtToken}");
				}
			}

			await _next(context);
		}

	}
}
