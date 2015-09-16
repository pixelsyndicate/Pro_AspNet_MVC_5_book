using System.Net.Http;
using System.Threading.Tasks;

namespace asyncDemo
{
    public class MyAsyncMethods
    {
        // TASK<> is a threading task
        public static Task<long?> GetPageLengthOldWay()
        {
            // create a mini web browser
            HttpClient client = new HttpClient();

            // HttpClient.GetAsync returns Task<HttpResponseMessage>
            Task<HttpResponseMessage> httpTask = client.GetAsync("http://apress.com");

            // we could do other things were while waiting for the HTTP request to complete

            // continueWith means only continue when the task completes (antecedent means a thing that was preexisting or previous)
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
            {
                return antecedent.Result.Content.Headers.ContentLength; // odd. we have two returns?
            });

            // or we can do it with Lambda
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => antecedent.Result.Content.Headers.ContentLength);
        }


        // applying async and await keywords to simplify the ContinueWith conundrum
        public async static Task<long?> GetPageLengthNewWay()
        {
            // create a mini web browser
            HttpClient client = new HttpClient();

            // await HttpClient.GetAsync returns HttpResponseMessage (no longer threading task containing it eg. Task<HttpResponseMessage>)
            HttpResponseMessage httpMessage = await client.GetAsync("http://apress.com");

            // we could do other things were while waiting for the HTTP request to complete

            // now we don't need .ContinueWith((Task<HttpResponseMessage> antecedent) => antecedent.Result.Content.Headers.ContentLength)
            return httpMessage.Content.Headers.ContentLength;

        }
    }
}