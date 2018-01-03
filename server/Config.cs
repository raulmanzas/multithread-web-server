using System;
using System.Linq;

namespace WebServer
{
    /*
        Objeto que controla as configurações de funcionamento do servidor.
     */
    class Config
    {
        // Número de threads disponíveis no threadpool.
        public int NumberOfThreads { get; set; }
        public int BufferSize { get; set; }
        
        // Diretório raíz que o servidor usará para buscar arquivos requisitados.
        public string StaticFilesDirectory { get; set; }

        // Quais tipos de arquivos o servidor pode enviar?
        public string[] ValidExtensions { get; set; }

        // Url de base para comunicação com o servidor.
        public string BaseUrl { get; set; }
        
        public void Validate()
        {
            if(NumberOfThreads == 0)
                throw new Exception("O número de threads deve ser > 0");
            if(String.IsNullOrEmpty(StaticFilesDirectory))
                throw new Exception("Diretório de arquivos estáticos é inválido!");
            if(ValidExtensions.Length == 0)
                throw new Exception("Extensões permitidas inválidas!");
            if(ValidExtensions.Any(extensao => extensao.EndsWith("*")))
                throw new ArgumentException("Extensão '.*' inválida!");
            if(String.IsNullOrEmpty(BaseUrl))
                throw new Exception("URL de base é inválida!");
        }
    }

}