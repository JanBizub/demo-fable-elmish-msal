namespace AADDemo.API2
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.Authentication.JwtBearer;

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddCors(
          fun p -> p.AddPolicy(
                      name = "AllowAllCors",
                      configurePolicy = fun builder ->
                        builder
                         .AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader() |> ignore)) |> ignore
        
        services
          .AddAuthentication(fun o -> o.DefaultScheme <- JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer     (fun o ->
            o.Authority <- "https://login.microsoftonline.com/<TENANT ID>/v2.0" 
            o.Audience  <- "<CLIENT ID>"
          ) |> ignore

        services.AddControllers() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseCors("AllowAllCors") |> ignore
        app.UseAuthentication()     |> ignore 
        app.UseAuthorization()      |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
