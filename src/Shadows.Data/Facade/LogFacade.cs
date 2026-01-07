using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Data.Facade
{
    public class LogFacade
    {
        private static string GetFilePath()
        {
            DirectoryInfo diChars = AppFiles.GetCharactersDir();
            string filePath = Path.Combine(diChars.FullName, "log.txt");
            return filePath;
        }

        public static void Add(string method, string message)
        {
            FileInfo fileInfo = new FileInfo(GetFilePath());
            if (fileInfo.Exists) { fileInfo.Delete(); }

            string t = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");

            TextWriter textWriter = new StreamWriter(GetFilePath());
            textWriter.WriteLine($"{t}, {method}: {message}");
            textWriter.Close();
        }

        public static string Get()
        {
            string message = String.Empty;

            FileInfo fileInfo = new FileInfo(GetFilePath());
            if (fileInfo.Exists) 
            {
                TextReader reader = new StreamReader(fileInfo.FullName);
                message = reader.ReadToEnd();
                reader.Close();
            }

            return message;
        }
    }
}
