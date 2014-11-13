namespace Cloud_Vibe.Web.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}