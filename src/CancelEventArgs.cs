using System;
namespace AppBlocks.Models
{
    public class CancelEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
    }
}