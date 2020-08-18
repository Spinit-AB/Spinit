# ApiVersionOperationFilter
Example usage:

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                _provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(
                    description.GroupName,
                    new Info
                    {
                        Title = "Some Title",
                        Version = description.GroupName,
                        Description = description.IsDeprecated ? "This API version has been deprecated." : string.Empty
                    });
            }

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();

                if (actionApiVersionModel == null)
                {
                    return true;
                }

                if (actionApiVersionModel.DeclaredApiVersions.Any())
                {
                    return actionApiVersionModel.DeclaredApiVersions.Any(v => docName == $"v{v.ToString()}");
                }

                return actionApiVersionModel.ImplementedApiVersions.Any(v => docName == $"v{v.ToString()}");
            });

            c.OperationFilter<ApiVersionOperationFilter>();
        });
    }

# BasicAuthMiddleware
This is used to put a simple username/password on swagger.
Configuration:

    "SwaggerAuth": {
        "Username": "test",
        "Password": "12345",
        "SkipAuthLocally": false
    },

Example usage:

    // Use only for swagger paths
    app.UseWhen(
        context => context.Request.Path.StartsWithSegments(new PathString("/swagger"), StringComparison.InvariantCultureIgnoreCase),
        app => app.UseMiddleware<SwaggerBasicAuthMiddleware>(Configuration));

    // Use the normal authentication for all other paths
    app.UseWhen(
        context => !context.Request.Path.StartsWithSegments(new PathString("/swagger"), StringComparison.InvariantCultureIgnoreCase),
        app => app.UseAuthentication());

