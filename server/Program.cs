using System;
using System.Net;
using System.Text;
using System.Threading;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Listener("http://localhost:4043/", RequestHandler);
            server.Listen();
            Console.WriteLine("Listening...");
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
