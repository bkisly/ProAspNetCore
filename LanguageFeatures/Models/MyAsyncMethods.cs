namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        public static async IAsyncEnumerable<long?> GetPageLengthsAsync(List<string> output, params string[] urls)
        {
            HttpClient client = new();

            foreach (var url in urls)
            {
                output.Add($"Started request for {url}");
                var httpMessage = await client.GetAsync($"http://{url}");
                output.Add($"Finished request for {url}");
                yield return httpMessage.Content.Headers.ContentLength;
            }
        }
    }
}
