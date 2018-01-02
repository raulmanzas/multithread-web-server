using System;
using System.Net;
using System.Text;
using System.Threading;

namespace WebServer
{
    class Program
    {
        const int NUMBER_OF_THREADS_INDEX = 0;
        const int BUFFER_SIZE_INDEX = 1;

        static void Main(string[] args)
        {
            var server = new Listener("http://localhost:4043/", RequestHandler);
            try
            {
                ValidateParameters(args);
                ConfigureThreadPool(args);
                Console.WriteLine("Listening...");
                server.Listen();
                server.Finish();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                server.Finish();
                return;
            }
        }

        private static void ValidateParameters(string[] args)
        {
            if(args.Length != 2)
                throw new ArgumentException("Você deve passar exatamente dois parâmetros!");
            if(args[0] == null)
                throw new ArgumentException("Número máximo de threads inválido!");
            if(args[1] == null)
                throw new ArgumentException("Tamanho de buffer inválido!");
        }

        /*
         Implementa as configurações básicas da thread pool: número de threads e
         e tamanho da fila de tarefas.
         */
        private static void ConfigureThreadPool(string[] args)
        {
            int numberOfThreads = Int32.Parse(args[NUMBER_OF_THREADS_INDEX]);
            ThreadPool.SetMaxThreads(numberOfThreads, 0);
        }

        /*
            Método que é injetado no servidor e é executado todas as vezes em que 
            uma nova requisição é recebida. Trata e responde as requisições.
        */
        public static void RequestHandler(HttpListenerContext context){
            int thread = Thread.CurrentThread.ManagedThreadId;
            string responseBody = $"<h1>It works! <br> Thread {thread} </h1>";
            byte[] byteArray = Encoding.UTF8.GetBytes(responseBody);
            context.Response.ContentLength64 = byteArray.Length;
            context.Response.OutputStream.Write(byteArray, 0, byteArray.Length);
        }
    }
}
