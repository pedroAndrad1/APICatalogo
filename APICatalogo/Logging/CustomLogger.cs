
namespace APICatalogo.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
        {
            this.loggerName = loggerName;
            this.loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return loggerConfig.LogLevel == logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
           string texto = $"{logLevel}: {eventId.Id} - {formatter(state, exception)}";
           EscreverTextoNoArquivo(texto);
        }

        private void EscreverTextoNoArquivo(string texto) {
            string caminhoArquivoLog = @"C:\Users\T-Gamer\Desktop\estudos\.Net\logs";

            using(StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true)) 
            {
                try
                {
                    streamWriter.WriteLine(texto);
                    streamWriter.Close();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}
