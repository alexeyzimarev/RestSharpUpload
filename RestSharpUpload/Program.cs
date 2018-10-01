using System.IO;
using RestSharp;

namespace RestSharpUpload
{
    class Program
    {
        static void Main()
        {
            var currentDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("upload")
                .AddFile("hello.txt", Path.Combine(currentDirectory, "hello.txt"));

            client.Put(request);
        }
    }
}