using System;

namespace RetroRedo.Exceptions
{
    internal class MapNotExistException : Exception
    {
        public override string Message => "The requested map doesn't exist.";
    }
}