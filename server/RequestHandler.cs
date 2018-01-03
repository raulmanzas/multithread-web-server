using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace WebServer
{
    class RequestHandler
    {
        private string _basePath;
        private IList<string> _validExtensions;

        public RequestHandler(Config configObj)
        {
            _basePath = configObj.StaticFilesDirectory;
            _validExtensions = configObj.ValidExtensions;
        }

        public void Handler(HttpListenerContext context)
        {
#if DEBUG
            int thread = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread {thread} executando...");
#endif
            try
            {
                var response = LoadFile(context.Request.Url);
                context.Response.ContentLength64 = response.Length;
                context.Response.OutputStream.Write(response, 0, response.Length);
            }
            catch(Exception e)
            {
                HandleError(context.Response, e);
            }
        }

        private void HandleError(HttpListenerResponse response, Exception e)
        {
            if(e is FileNotFoundException)
                response.StatusCode = (int) HttpStatusCode.NotFound;
            else
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var errorMessage = Encoding.UTF8.GetBytes(e.Message);
            response.ContentEncoding = Encoding.UTF8;
            response.ContentLength64 = errorMessage.Length;
            response.OutputStream.Write(errorMessage, 0, errorMessage.Length);
        }

        private byte[] LoadFile(Uri path)
        {
            string localPath = _basePath + path.AbsolutePath;
            if(_validExtensions.Any(ext => localPath.Contains(ext)))
                return File.ReadAllBytes(localPath);
            throw new Exception("Tipo de arquivo solicitado não é permitido.");
        }
    }
}