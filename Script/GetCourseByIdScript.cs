using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class GetCourseByIdScript
    {
        private readonly IGraphQLClient _client;

        public GetCourseByIdScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            IOperationResult<IGetCourseByIdResult> courseByIdResult = await _client.GetCourseById.ExecuteAsync(Guid.Parse("88a32d29-2673-4512-efbc-08da9259c762"));
            if (courseByIdResult.IsErrorResult())
            {
                Console.WriteLine("Failed to get course");
            }
            else
            {
                IGetCourseById_CourseById course = courseByIdResult.Data.CourseById;
                Console.WriteLine($"{course?.Id} - {course?.Name} - {course?.Instructor.FirstName} - {course?.Students?.Count}");
            }
        }
    }
}
