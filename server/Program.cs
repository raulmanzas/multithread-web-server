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
                if(args.Length == 0)
                    throw new ArgumentException("Arquivo de configuração é obrigatório!");
                LoadConfigFile(args);
                ConfigureThreadPool();
                Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                _server?.Finish();
                return;
            }
        }

        private static void Run(){
            var requestHandler = new RequestHandler(_serverConfig);
            _server = new Listener(_serverConfig.BaseUrl, requestHandler.Handler);
#if DEBUG
            Console.WriteLine("Aguardando requisições...");
#endif
            _server.Listen();
        }

        /*
         Implementa as configurações básicas da thread pool: número de threads e
         e tamanho da fila de tarefas.
         */
        private static void ConfigureThreadPool()
        {
            int numberOfThreads = _serverConfig.NumberOfThreads;
            ThreadPool.SetMaxThreads(numberOfThreads, 0);
            ThreadPool.SetMinThreads(numberOfThreads, 0);
        }

        /*
            Carrega o arquivo responsável pelas configurações de funcionamento
            do servidor. Cada configuração possível é uma propriedade da classe
            Config.
        */
        private static void LoadConfigFile(string[] args)
        {
            var file = File.ReadAllText(args[0]);
            _serverConfig = JsonConvert.DeserializeObject<Config>(file);
            _serverConfig.Validate();
        }
    }
}
