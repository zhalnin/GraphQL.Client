namespace GraphQL.Client.Script
{
    public class SubscribeCreateCourseScript
    {
        private readonly IGraphQLClient _client;

        public SubscribeCreateCourseScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            _client.CourseCreated.Watch().Subscribe(result =>
            {
                string name = result.Data.CourseCreated.Name;
                Console.WriteLine($"Course {name} was created");
            });
        }
    }
}
