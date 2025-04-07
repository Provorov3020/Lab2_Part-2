using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Инициализация и запуск таймера для измерения времени выполнения программы.

        HttpClient client = new HttpClient(); // Создание экземпляра HttpClient для отправки HTTP-запросов.

        // Запуск асинхронных задач для получения ответов с сервера.
        Task<string> firstResponse = GetResponseFromServerAsync("https://jsonplaceholder.typicode.com/posts");
        Task<string> secondResponse = GetResponseFromServerAsync("https://jsonplaceholder.typicode.com/comments");
        Task<string> thirdResponse = GetResponseFromServerAsync("https://jsonplaceholder.typicode.com/albums");

        // Ожидание завершения всех асинхронных вызовов и получение результатов в массиве responses.
        string[] responses = await Task.WhenAll(firstResponse, secondResponse, thirdResponse);

        // Вывод каждого ответа в консоль.
        foreach (var response in responses)
        {
            Console.WriteLine(response);
        }

        // Остановка таймера после всех запросов.
        stopwatch.Stop();

        // Вывод общего времени выполнения, прошедшего в миллисекундах.
        Console.WriteLine($"Общее время работы запросов: {stopwatch.ElapsedMilliseconds} мс");

        static async Task<string> GetResponseFromServerAsync(string url) // Асинхронный метод для получения ответа от сервера по указанному URL.
        {
            using HttpClient httpClient = new HttpClient(); // Создание нового экземпляра HttpClient для текущего запроса.

            try
            {
                Console.WriteLine($"Запрос к серверу: {url}"); // Сообщение о начале запроса.
                string response = await httpClient.GetStringAsync(url); // Асинхронный вызов получения строки ответа от сервера.
                return response; // Возврат полученного ответа
            }
            catch (HttpRequestException e)
            {
                return $"Ошибка при запросе к {url}: {e.Message}"; // Возврат сообщения об ошибке.
            }
            catch (Exception e)
            {
                return $"Непредвиденная ошибка при запросе к {url}: {e.Message}";
            }

        }
    }
}









