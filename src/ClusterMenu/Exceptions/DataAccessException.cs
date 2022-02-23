using System;

namespace ClusterMenu.Exceptions {
    public class DataAccessException : ApplicationException {

        public DataAccessException() { }

        public DataAccessException(string message) : base(message) { }

    }
}
