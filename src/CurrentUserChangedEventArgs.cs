using System;

namespace AppBlocks.Models
{
    public class CurrentUserChangedEventArgs : EventArgs
    {
        public CurrentUserChangedEventArgs(string message)
        {
            Message = message;
        }
        public string Message;
    }
}