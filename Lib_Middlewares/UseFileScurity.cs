using Microsoft.AspNetCore.Http;
using TrendyT_Data.Identity;

public class UseFileScurity
{
    private readonly RequestDelegate _next;

    public UseFileScurity(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Kiểm tra nếu đường dẫn bắt đầu bằng "/img"
        if (context.Request.Path.StartsWithSegments("/img"))
        {
            // Kiểm tra quyền truy cập tại đây
            if (!context.User.HasClaim(c =>
                    c.Type == IdentityData.TypeRole &&
                    (c.Value == IdentityData.IndustryClaimName ||
                     c.Value == IdentityData.SecretaryClaimName)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access Denied");
                return;
            }
        }

        await _next(context);
    }
}
