using Firebase.Auth;
using GraphQL.Api.Services;
using GraphQL.Client.Script;
using GraphQL.Client.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddGraphQLClient()
            .ConfigureHttpClient((services, c) => {
                c.BaseAddress = new Uri(context.Configuration.GetValue<string>("HTTP_GRAPHQL_API_URL"));
                TokenStore tokenStore = services.GetService<TokenStore>();
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);
            })
            .ConfigureWebSocketClient(c => c.Uri = new Uri(context.Configuration.GetValue<string>("WS_GRAPHQL_API_URL")));
        services.AddHostedService<StartupService>();
        services.AddSingleton(new FirebaseAuthProvider(new FirebaseConfig(context.Configuration.GetValue<string>("FIREBASE_API_KEY"))));
        services.AddSingleton<TokenStore>();
        services.AddTransient<GetPaginatedCoursesScript>();
        services.AddTransient<GetCoursesScript>();
        services.AddTransient<GetCourseByIdScript>();
        services.AddTransient<CreateCourseScript>();
        services.AddTransient<UpdateCourseScript>();
        services.AddTransient<LoginScript>();
        services.AddTransient<SearchScript>();
        services.AddTransient<DeleteCourseScript>();
        services.AddTransient<SubscribeCreateCourseScript>();
        services.AddTransient<SubscribeUpdateCourseScript>();
    })
    .Build()
    .Run();
