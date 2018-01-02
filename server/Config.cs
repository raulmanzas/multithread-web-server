using System;

namespace WebServer
{
    class Config
    {
        public int NumberOfThreads { get; set; }
        public int BufferSize { get; set; }
        public string StaticFilesDirectory { get; set; }
        public string[] ValidExtensions { get; set; }
        public string BaseUrl { get; set; }
    }
}