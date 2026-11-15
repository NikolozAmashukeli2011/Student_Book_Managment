using Student_Book_Managment.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Student_Book_Managment.DataHandlers
{
    internal class JsonFIleStorage
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions() { WriteIndented = true };

        public async static Task Save<T>(T data, string path)
        {
            string json = JsonSerializer.Serialize(data, Options);

            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(json);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Couldn't save. {ex.Message}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"You don't have permission to modify the file. {ex.Message}.");
            }
        }

        public async static Task<T> Load<T>(string path) where T: new()
        {
            if (!File.Exists(path)) return new T();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string json = await sr.ReadToEndAsync();
                    return JsonSerializer.Deserialize<T>(json, Options) ?? new T();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Couldn't load. {ex.Message}.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"You don't have permission to access this file. {ex.Message}.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"The text isn't a valid JSON. {ex.Message}.");
            }

            return new T();
        }
    }
}
