using System;

namespace Graph.Utility.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);
    }
}
