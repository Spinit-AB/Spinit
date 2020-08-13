using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Spinit.Web.Swagger.ApiVersion
{
    public static class Extensions
    {
        public static void AddApiVersion(this SwaggerGenOptions options)
        {
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
