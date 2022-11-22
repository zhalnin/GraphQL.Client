using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class GetCoursesScript
    {
        private readonly IGraphQLClient _client;

        public GetCoursesScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            IOperationResult<IGetCoursesResult> result = await _client.GetCourses.ExecuteAsync();
            if (result.IsErrorResult())
            {
                Console.WriteLine("Failed to get courses");
            }
            else
            {
                foreach (var course in result.Data.Courses)
                {
                    Console.WriteLine($"{course.Id} - {course.Name} - {course.Subject}");
                }
            }
        }
    }
}
