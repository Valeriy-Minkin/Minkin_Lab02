using System;

namespace Minkin_Lab02
{
    public class FileSystemError:Exception
    {
        public Exception InnerExceptionInstance { get; }
        public string MessageText { get; }

        public FileSystemError()
        {

        }

        public FileSystemError(string message, Exception exception)
        {
            MessageText = message;
            InnerExceptionInstance = exception;
        }
    }
}
