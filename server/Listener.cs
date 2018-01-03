using System;
using System.Net;
using System.Text;
using System.Threading;

namespace WebServer
{
    /*
        Classe que implementa um servidor HTTP. Após instanciada, fica esperando por 
        requisições na URL passada e as enfileira na fila de execuções da ThreadPool.

        TODO:
            - Verificar até quando as requisições são enfileiradas
              antes de bloquear a thread principal.
            - Tirar dúvida sobre tratamento das requests.
     */
    class Listener
    {
        private string _urlDefault = "http://localhost:8080";
        private HttpListener _server;
        private Action<HttpListenerContext> _requestHandler;
        private string _url;

        public Listener(string url, Action<HttpListenerContext> requestHandler)
        {
            _url = String.IsNullOrEmpty(url) ? _urlDefault : url;
            _server = new HttpListener();
            _server.Prefixes.Add(_url);
            _requestHandler = requestHandler;
        }

        /*
            Fica em loop aguardando requisições e as enfileirando para que as threads
            da ThreadPool possam trata-las através da delegate injetada.
            Ref: https://msdn.microsoft.com/pt-br/library/system.threading.threadpool(v=vs.110).aspx
        */
        public void Listen(){
            _server.Start();
            while(_server.IsListening)
            {
                HttpListenerContext requestContext = _server.GetContext();
                // Enfileira a requisição para a ThreadPool.
                ThreadPool.QueueUserWorkItem((state) => 
                {
                    _requestHandler(requestContext);
                });
            }
        }

        public void Finish()
        {
#if DEBUG
            Console.WriteLine("Finalizando servidor...");
#endif
            _server.Stop();
            _server.Close();
        }
    }
}