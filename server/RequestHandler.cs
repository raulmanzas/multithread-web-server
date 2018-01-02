using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace WebServer
{
    class RequestHandler
    {
        private string _basePath;
        private IList<string> _validExtensions;

        public RequestHandler(string basePath, IList<string> validExtensions)
        {
            ValidateExtensions(validExtensions);
            _basePath = String.IsNullOrEmpty(basePath) ? "/" : basePath;
            _validExtensions = validExtensions;
        }

        public void Handler(HttpListenerContext context)
        {
#if DEBUG
            int thread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread {thread} executando...");
#endif
            var responseFile = LoadFile(context.Request.Url);
            context.Response.ContentLength64 = responseFile.Length;
            context.Response.OutputStream.Write(responseFile, 0, responseFile.Length);
        }

        private byte[] LoadFile(Uri path)
        {
            //TODO: Validar a extensão do arquivo solicitado
            string localPath = Path.Combine(_basePath, path.AbsolutePath);
            return File.ReadAllBytes(localPath.Remove(0,1));
        }

        private void ValidateExtensions(IList<string> extensions)
        {
            if(!extensions.Any())
                throw new ArgumentException("Sem extensões válidas!");
            if(extensions.Any(extensao => extensao.EndsWith("*")))
                throw new ArgumentException("Extensão '.*' inválida!");
            if(extensions.Any(extensao => extensao.Contains("../")))
                throw new ArgumentException("Extensão contém '../'!");
        }
    }
}