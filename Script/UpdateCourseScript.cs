using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class UpdateCourseScript
    {
        private readonly IGraphQLClient _client;

        public UpdateCourseScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            Console.WriteLine("Enter course'id to update:");
            Guid courseId = Guid.Parse(Console.ReadLine());

            CourseTypeInput courseInput = new CourseTypeInput
            {
                Name = "GraphQL 102",
                Subject = Subject.Science,
                InstructorId = Guid.Parse("8B663C84-D2C7-4031-9285-BADF41EE82CF")
            };

            IOperationResult<IUpdateCourseResult> updateCourseResult = await _client.UpdateCourse.ExecuteAsync(courseId, courseInput);

            if (updateCourseResult.IsErrorResult())
            {
                IClientError error = updateCourseResult.Errors.First();
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
                string updatedCourseName = updateCourseResult.Data.UpdateCourse.Name;
                Console.WriteLine($"Successfully updated course to {updatedCourseName}");
            }
        }
    }
}
