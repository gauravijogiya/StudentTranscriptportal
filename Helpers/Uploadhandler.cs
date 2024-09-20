
using StudentTranscriptPortal.Models;
using System.IO;

namespace StudentTranscriptPortal.Helpers
{
    public class Uploadhandler(String studentName,IFormFile file) 
    {
        public string Upload(IFormFile file)
        {
            //check for extension
            List<string> lstValidExtensions = new List<string>() { ".pdf", ".jpg", ".png" };
            string extension = Path.GetExtension(file.FileName);
           
            if (!lstValidExtensions.Contains(extension))
            {
                return $"Extention is not valid ("+string.Join(',', lstValidExtensions) +")";
            }

            //Check for size
            long filesize = file.Length;
            if (filesize > (10 * 1014 * 1024)) {
                return $"MAximum allowed size is 10MB"; 
            }

            //SavingFile
            //    string fileName = file.FileName + extension;
            //    string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            //    using FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            //file.CopyTo(stream);
            // Define the path to store the uploaded transcript
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // Create a unique file name for the uploaded PDF
            var fileName = $"{studentName}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }


            return fileName;

            }
        }


    }
    

