using Microsoft.AspNetCore.Mvc.Routing;

namespace RequestLocalization.zz_TeBekijken;

internal class MyTransformer : DynamicRouteValueTransformer
{
    public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        throw new NotImplementedException();
    }
}

internal class waaat
{
    public static void foo(IEndpointRouteBuilder app)
    {
        app.MapDynamicControllerRoute<MyTransformer>(string.Empty);
    }

}