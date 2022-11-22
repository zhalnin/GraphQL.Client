using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class DeleteCourseScript
    {
        private readonly IGraphQLClient _client;

        public DeleteCourseScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            Console.WriteLine("Enter course'id to delete:");
            Guid courseId = Guid.Parse(Console.ReadLine());

            IOperationResult<IDeleteCourseResult> deleteCourseResult = await _client.DeleteCourse.ExecuteAsync(courseId);
            if (deleteCourseResult.IsErrorResult())
            {
                IClientError error = deleteCourseResult.Errors.First();
                if (error.Code == "AUTH_NOT_AUTHENTICATED")
                {
                    Console.WriteLine($"The user is not authenticated");
                }
                else if (error.Code == "COURSE_NOT_FOUND")
                {
                    Console.WriteLine($"Course not found");
                }
                else
                {
                    Console.WriteLine($"Unkonwn course insert error");
                }
            }
            else
            {
                bool deleteCourseSuccessful = deleteCourseResult.Data.DeleteCourse;
                if (deleteCourseSuccessful)
                {
                    Console.WriteLine($"Successfully deleted course {courseId}");
                }
            }
        }
    }
}
