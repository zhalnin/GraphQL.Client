using GraphQL.Client;
using GraphQL.Client.Script;
using Microsoft.Extensions.Hosting;

namespace GraphQL.Api.Services
{
    public class StartupService : IHostedService
    {
        private readonly IGraphQLClient _client;
        private readonly LoginScript _loginScript;
        private readonly GetCoursesScript _getCoursesScript;
        private readonly GetCourseByIdScript _getCourseByIdScript;
        private readonly GetPaginatedCoursesScript _getPaginatedCoursesScript;
        private readonly CreateCourseScript _createCourseScript;
        private readonly UpdateCourseScript _updateCourseScript;
        private readonly SearchScript _searchScript;
        private readonly DeleteCourseScript _deleteCourseScript;
        private readonly SubscribeCreateCourseScript _subscribeCreateCourseScript;
        private readonly SubscribeUpdateCourseScript _subscribeUpdateCourseScript;

        public StartupService(IGraphQLClient client
            , GetCoursesScript getCoursesScript
            , GetCourseByIdScript getCourseByIdScript
            , GetPaginatedCoursesScript getPaginatedCoursesScript
            , CreateCourseScript createCourseScript
            , UpdateCourseScript updateCourseScript
            , LoginScript loginScript
            , SearchScript searchScript
            , DeleteCourseScript deleteCourseScript
            , SubscribeCreateCourseScript subscribeCreateCourseScript
            , SubscribeUpdateCourseScript subscribeUpdateCourseScript) =>
            (_client, _getCoursesScript, _getCourseByIdScript, _getPaginatedCoursesScript, _createCourseScript, _updateCourseScript, _loginScript, _searchScript, _deleteCourseScript, _subscribeCreateCourseScript, _subscribeUpdateCourseScript) = 
            (client, getCoursesScript, getCourseByIdScript, getPaginatedCoursesScript, createCourseScript, updateCourseScript, loginScript, searchScript, deleteCourseScript, subscribeCreateCourseScript, subscribeUpdateCourseScript);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await _loginScript.Run();
            //await _getCoursesScript.Run();
            //await _getCourseByIdScript.Run();
            //await _getPaginatedCoursesScript.Run();
            //await _createCourseScript.Run();
            //await _updateCourseScript.Run();
            //await _deleteCourseScript.Run();
            //await _subscribeCreateCourseScript.Run();
            //await _subscribeUpdateCourseScript.Run();
            await _searchScript.Run();

            Console.ReadKey();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
