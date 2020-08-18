# ApiVersion
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
            c.AddApiVersion(_provider, "Example API");
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

