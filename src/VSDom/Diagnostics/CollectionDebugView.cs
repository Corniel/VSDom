using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VSDom.Diagnostics
{
    /// <summary>Allows the debugger to display collections.</summary>
    internal class CollectionDebugView<T>
    {
        /// <summary>Constructor.</summary>
        public CollectionDebugView(IEnumerable<T> enumeration)
        {
            Enumeration = Guard.NotNull(enumeration, "enumeration");
        }

        /// <summary>The array that is shown by the debugger.</summary>
        /// <remarks>
        /// Every time the enumeration is shown in the debugger, a new array is created.
        /// By doing this, it is always in sync with the current state of the enumeration.
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
#pragma warning disable S2365 // Properties should not make collection or array copies
        // only way to let a debugger view show the actual collection.
        public T[] Items => Enumeration.ToArray();
#pragma warning restore S2365 // Properties should not make collection or array copies

        /// <summary>A reference to the enumeration to display.</summary>
        private readonly IEnumerable<T> Enumeration;
    }
}
