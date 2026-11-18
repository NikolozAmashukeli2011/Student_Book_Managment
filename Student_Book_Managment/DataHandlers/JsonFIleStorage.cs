using System.Text.Json;

namespace Student_Book_Managment.DataHandlers
{
    static class JsonFIleStorage // one class having 2 generic async methods, that handle all the file work.
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions() { WriteIndented = true };

        public async static Task SaveAsync<T>(T data, string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, Options);
                await File.WriteAllTextAsync(path, json);
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

        public async static Task<T> LoadAsync<T>(string path) where T : new() // T : new() means T must be a type with an empty constructor.
        {
            if (!File.Exists(path)) return new T();

            try
            {
                string json = await File.ReadAllTextAsync(path);
                return JsonSerializer.Deserialize<T>(json, Options) ?? new T();
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
