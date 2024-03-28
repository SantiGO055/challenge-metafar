namespace challenge_metafar.Abstractions
{
    public interface IEndpointDefinition
    {
        void RegisterEndpoints(WebApplication app, WebApplicationBuilder builder);
    }
}
