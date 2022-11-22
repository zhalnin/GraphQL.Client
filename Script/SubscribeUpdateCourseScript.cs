namespace GraphQL.Client.Script
{
    public class SubscribeUpdateCourseScript
    {
        private readonly IGraphQLClient _client;

        public SubscribeUpdateCourseScript(IGraphQLClient client) => _client = client;

        public async Task Run()
        {
            Console.WriteLine("Enter course'id to subscribe:");
            Guid courseId = Guid.Parse(Console.ReadLine());

            //courseId = Guid.Parse("6dedfc7b-5fb8-4764-3bba-08dab9ced3b8");
            _client.CourseUpdated.Watch(courseId).Subscribe(result =>
            {
                string name = result.Data.CourseUpdated.Name;
                Console.WriteLine($"Course{courseId} was renamed to {name}.");
            });
        }
    }
}
