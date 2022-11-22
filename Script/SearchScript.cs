using StrawberryShake;

namespace GraphQL.Client.Script
{
    public class SearchScript
    {
        private readonly IGraphQLClient _client;

        public SearchScript(IGraphQLClient client) =>_client=client;

        public async Task Run()
        {
            Console.WriteLine("Enter a search term");
            string term = Console.ReadLine();

            IOperationResult<ISearchResult> searchResult = await _client.Search.ExecuteAsync(term);

            IEnumerable<ISearch_Search_CourseType> courses = searchResult.Data.Search.OfType<ISearch_Search_CourseType>();

            Console.WriteLine("COURSES");
            foreach(ISearch_Search_CourseType course in courses)
            {
                Console.WriteLine(course.Name);
            }

            Console.WriteLine("");

            IEnumerable<ISearch_Search_InstructorType> instructors = searchResult.Data.Search.OfType<ISearch_Search_InstructorType>();
            Console.WriteLine("INSTRUCTORS");
            foreach(ISearch_Search_InstructorType instructor in instructors)
            {
                Console.WriteLine($"{instructor.LastName}, {instructor.FirstName}");
            }


            Console.WriteLine("");
        }
    }
}
