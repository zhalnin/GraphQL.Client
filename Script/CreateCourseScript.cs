using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class CreateCourseScript
    {
        private readonly IGraphQLClient _client;

        public CreateCourseScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            CourseTypeInput courseInput = new CourseTypeInput
            {
                Name = "Algebra",
                Subject = Subject.Mathematics,
                InstructorId = Guid.Parse("8B663C84-D2C7-4031-9285-BADF41EE82CF")
            };
            IOperationResult<ICreateCourseResult> createCourseResult = await _client.CreateCourse.ExecuteAsync(courseInput);


            if (createCourseResult.IsErrorResult())
            {
                IClientError error = createCourseResult.Errors.First();
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
                Guid courseId = createCourseResult.Data.CreateCourse.Id;
                string createdCourseName = createCourseResult.Data.CreateCourse.Name;
                Console.WriteLine($"Successfully created course {createdCourseName}");
            }
        }
    }
}
