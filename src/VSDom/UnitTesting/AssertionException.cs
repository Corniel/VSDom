using System;
using System.Runtime.Serialization;

namespace VSDom.UnitTesting
{
	/// <summary>Thrown when an assertion failed.</summary>
	[Serializable]
	public class AssertionException : Exception
	{
		/// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
		public AssertionException() { }

		/// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
		/// <param name="message">
		/// The error message that explains the reason for the exception.
		/// </param>
		public AssertionException(string message) : base(message) { }

		/// <summary>Creates a new instance of an <see cref="AssertionException"/>.</summary>
		/// <param name="message">
		/// The error message that explains the reason for the exception.
		/// </param>
		/// <param name="inner">
		/// The exception that caused the current exception to occur.
		/// </param>
		public AssertionException(string message, Exception inner) : base(message, inner) { }

		/// <summary>Serialization Constructor.</summary>
		protected AssertionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
