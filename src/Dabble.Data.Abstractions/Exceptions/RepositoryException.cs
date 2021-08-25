using System;

// ReSharper disable once CheckNamespace
namespace Dabble.Data.Abstractions
{
    /// <summary>
    /// Indicates an error in the operations on an entity repository
    /// </summary>
    public abstract class RepositoryException : Exception
    {
        /// <inheritdoc />
        protected RepositoryException(string message)
            : base(message) { }
    }
}
