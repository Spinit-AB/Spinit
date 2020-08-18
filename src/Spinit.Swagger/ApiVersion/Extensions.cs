using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Spinit.Web.Swagger.ApiVersion
{
    public static class Extensions
    {
        public static void AddApiVersion(this SwaggerGenOptions options, IApiVersionDescriptionProvider provider, string apiTitle)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new Info
                    {
                        Title = apiTitle,
                        Version = description.GroupName,
                        Description = description.IsDeprecated ? "This API version has been deprecated." : string.Empty
                    });
            }

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                if (actionApiVersionModel == null)
                {
                    return true;
                }

                if (actionApiVersionModel.DeclaredApiVersions.Any())
                {
                    return actionApiVersionModel.DeclaredApiVersions.Any(v => docName == $"v{v}");
                }

                return actionApiVersionModel.ImplementedApiVersions.Any(v => docName == $"v{v}");
            });

            options.OperationFilter<ApiVersionOperationFilter>();
        }

        private static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                .Where(x => ((Type)x.Key).Equals(typeof(ApiVersionModel)))
                .Select(x => x.Value as ApiVersionModel).FirstOrDefault();
        }
    }
}
