using System;
using System.Runtime.Serialization;

namespace VSDom.TestTools
{
    /// <summary>Thrown when an assertion failed.</summary>
    [Serializable]
    public class AssertionException : Exception
    {
        /// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
        public AssertionException(string message)
            : base(message) { }

        /// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
        public AssertionException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
        protected AssertionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <inheritdoc />
        public override string ToString()
        {
            return InnerException is null
                ? Message
                : base.ToString();
        }
    }
}
