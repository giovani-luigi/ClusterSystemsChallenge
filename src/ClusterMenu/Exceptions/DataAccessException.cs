using System;

namespace ClusterMenu.Exceptions {
    
    /// <summary>
    /// Represents an exception ocurred when accessing data
    /// </summary>
    public class DataAccessException : ApplicationException {

        public DataAccessException() { }

        public DataAccessException(string message) : base(message) { }

        public DataAccessException(string message, Exception innerException) : base(message, innerException) { }

    }
}
