﻿using System;
using System.Diagnostics;

namespace VSDom
{
	/// <summary>Supplies input parameter guarding.</summary>
	/// <remarks>
	/// Supplying a Guard mechanism is not something that belongs in VSDOM. So, 
	/// although is a nice feature, we don't provide it anymore as we would have to
	/// add methods just because the sake of being complete.
	/// </remarks>
	internal static class Guard
	{
		/// <summary>Guards the parameter if not null, otherwise throws an argument (null) exception.</summary>
		/// <typeparam name="T">
		/// The type to guard, can not be a structure.
		/// </typeparam>
		/// <param name="param">
		/// The parameter to guard.
		/// </param>
		/// <param name="paramName">
		/// The name of the parameter.
		/// </param>
		[DebuggerStepThrough]
		public static T NotNull<T>([ValidatedNotNull]T param, string paramName) where T : class
		{
			if (ReferenceEquals(param, null))
			{
				throw new ArgumentNullException(paramName);
			}
			return param;
		}
		
		/// <summary>Marks the NotNull argument as being validated for not being null,
		/// to satisfy the static code analysis.
		/// </summary>
		/// <remarks>
		/// Notice that it does not matter what this attribute does, as long as
		/// it is named ValidatedNotNullAttribute.
		/// </remarks>
		[AttributeUsage(AttributeTargets.Parameter)]
		sealed class ValidatedNotNullAttribute : Attribute { }
	}
}