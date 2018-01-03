using System;
using System.Linq;

namespace WebServer
{
    class Config
    {
        public int NumberOfThreads { get; set; }
        public int BufferSize { get; set; }
        public string StaticFilesDirectory { get; set; }
        public string[] ValidExtensions { get; set; }
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