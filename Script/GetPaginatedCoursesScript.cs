using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class GetPaginatedCoursesScript
    {
        private readonly IGraphQLClient _client;

        public GetPaginatedCoursesScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            ConsoleKey key;

            int? first = 2;
            string after = null;

            int? last = null;
            string before = null;

            do
            {
                var coursesResult = await _client.GetPaginatedCourses.ExecuteAsync(first, after, last, before,
                new CourseTypeFilterInput
                {
                    Subject = new SubjectOperationFilterInput
                    {
                        Eq = Subject.Mathematics
                    }
                },
                new List<CourseTypeSortInput>
                {
                    new CourseTypeSortInput
                    {
                        Name = SortEnumType.Asc
                    }
                });

                if (coursesResult.IsErrorResult())
                {
                    IClientError error = coursesResult.Errors.First();
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
                    Console.WriteLine($"{"Name",20} | {"Subject",20}");

                    foreach (IGetPaginatedCourses_PaginatedCourses_Nodes course in coursesResult.Data.PaginatedCourses.Nodes)
                    {
                        Console.WriteLine($"{course.Name,20} |${course.Subject,20}");
                    }
                }

                IGetPaginatedCourses_PaginatedCourses_PageInfo pageInfo = coursesResult.Data.PaginatedCourses.PageInfo;

                if (pageInfo.HasPreviousPage)
                {
                    Console.WriteLine("Press 'A' to move to the previous page.");
                }

                if (pageInfo.HasNextPage)
                {
                    Console.WriteLine("Press 'D' to move to the next page.");
                }

                Console.WriteLine("Press 'Enter' to exit");

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.A && pageInfo.HasPreviousPage)
                {
                    last = 2;
                    before = pageInfo.StartCursor;

                    first = null;
                    after = null;
                }
                else if (key == ConsoleKey.D && pageInfo.HasNextPage)
                {
                    last = null;
                    before = null;

                    first = 2;
                    after = pageInfo.EndCursor;
                }
            }
            while (key != ConsoleKey.Enter);
        }
    }
}
