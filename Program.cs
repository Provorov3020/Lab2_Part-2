using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Инициализация и запуск таймера для измерения времени выполнения программы.

        HttpClient client = new HttpClient(); // Создание экземпляра HttpClient для отправки HTTP-запросов.

        try
        {
            // Параллельные задачи (по очереди, без async/await)
            Console.WriteLine("Start PrintId with id 1"); // Сообщение о начале выполнения метода.          
           

            Console.WriteLine("Start PrintId with id 2"); // Сообщение о начале выполнения метода.
           
           

            // Выполнение синхронных HTTP-запросов к серверам.
            string response1 = MakeRequest(client, "https://jsonplaceholder.typicode.com/posts");
            string response2 = MakeRequest(client, "https://jsonplaceholder.typicode.com/comments");
            string response3 = MakeRequest(client, "https://jsonplaceholder.typicode.com/albums");

            // Вывод результатов ответов от серверов.
            if (response1 != null) // Проверка на null, чтобы избежать ошибок при выводе.
            {
                Console.WriteLine("Ответ от сервера 1:");
                Console.WriteLine(response1);
            }
            if (response2 != null)
            {
                Console.WriteLine("Ответ от сервера 2:");
                Console.WriteLine(response2);
            }
            if (response3 != null)
            {
                Console.WriteLine("Ответ от сервера 3:");
                Console.WriteLine(response3);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}"); // Вывод сообщения об ошибке.

        }
        finally
        {
            client.Dispose(); // Освобождение ресурсов, занятых клиентом.
            stopwatch.Stop(); // Остановка таймера.
            Console.WriteLine($"Общее время работы: {stopwatch.ElapsedMilliseconds} мс"); // Вывод общего времени выполнения программы в миллисекундах.
        }
    }

    
    private static string MakeRequest(HttpClient client, string url) // Метод для выполнения HTTP-запроса.
    {
        var response = client.GetAsync(url).Result; // Синхронный HTTP-запрос к указанному URL(здесь используется Result для блокировки выполнения до получения результата).

        // Проверяем успешность ответа
        if (!response.IsSuccessStatusCode)
        {
            // Сообщаем об ошибке в консоль
            throw new Exception($"Ошибка при получении данных: {response.StatusCode} - {response.ReasonPhrase}"); // Генерация исключения с сообщением об ошибке.
        }

        return response.Content.ReadAsStringAsync().Result; // Считываем и возвращаем содержимое ответа
    }
}


       

        
    