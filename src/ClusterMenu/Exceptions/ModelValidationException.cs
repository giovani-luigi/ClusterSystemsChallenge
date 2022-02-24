using System;

namespace ClusterMenu.Exceptions {
    
    /// <summary>
    /// Exception thrown when a model fails the validation
    /// </summary>
    public class ModelValidationException : ApplicationException {

        public ModelValidationException() : base("Invalid data model") {
        }

        public ModelValidationException(string message) : base(message) {
        }

    }
}
