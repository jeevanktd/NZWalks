using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Versioning.API
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            ApiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        public IApiVersionDescriptionProvider ApiVersionDescriptionProvider { get; }

        void IConfigureNamedOptions<SwaggerGenOptions>.Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void  Configure(SwaggerGenOptions options)
        {
            foreach(var item in ApiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName,
                    CreateVersionInfo(item));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription des)
        {
            var info = new OpenApiInfo
            {
                Title = "Your Version API",
                Version = des.ApiVersion.ToString()
            };
            return info;
        }
    }
}
