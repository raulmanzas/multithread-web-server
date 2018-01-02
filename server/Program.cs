using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace WebServer
{
    class Program
    {
        private static Config _serverConfig;
        private static Listener _server;

        static void Main(string[] args)
        {
            try
            {
                if(args[0] == null)
                    throw new ArgumentException("Arquivo de configuração é obrigatório!");
                LoadConfigFile(args);
                ConfigureThreadPool();
                Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                _server.Finish();
                return;
            }
        }

        private static void Run(){
            var requestHandler = new RequestHandler(_serverConfig);
            _server = new Listener(_serverConfig.BaseUrl, requestHandler.Handler);
            Console.WriteLine("Aguardando requisições...");
            _server.Listen();
            _server.Finish();
        }

        /*
         Implementa as configurações básicas da thread pool: número de threads e
         e tamanho da fila de tarefas.
         */
        private static void ConfigureThreadPool()
        {
            //TODO: Verificação de configuração do tamanho do buffer
            int numberOfThreads = _serverConfig.NumberOfThreads;
            ThreadPool.SetMaxThreads(numberOfThreads, 0);
        }

        private static void LoadConfigFile(string[] args)
        {
            var file = File.ReadAllText(args[0]);
            _serverConfig = JsonConvert.DeserializeObject<Config>(file);
            _serverConfig.Validate();
        }
    }
}
