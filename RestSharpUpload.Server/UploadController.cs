using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestSharpUpload.Server
{
    [Route("upload")]
    public class UploadController : Controller
    {
        [HttpPut]
        public async Task<IActionResult> Put(IFormCollection form)
        {
            var files = form.Files;
            
            long size = files.Sum(f => f.Length);
            var names = new StringBuilder();
            var currentDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            foreach (var formFile in files)
            {
                if (formFile.Length <= 0) continue;
                names.Append(formFile.Name + "\r\n");

                var fullName = Path.Combine(currentDirectory, formFile.Name);
                Console.WriteLine(fullName);
                using (var stream = new FileStream(fullName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new {count = files.Count, size, names = names.ToString()});
        }
    }
}