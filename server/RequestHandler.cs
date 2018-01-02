using System;
using System.Collections.Generic;
using System.Linq;

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