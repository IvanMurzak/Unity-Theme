using System;
using UnityEngine;

namespace Unity.Theme.Binders
{
    /// <summary>
    /// Wrapper class for a fixed-size array of MultiColorBinderEntry.
    /// This prevents users from modifying the array size in the Unity Inspector.
    /// </summary>
    [Serializable]
    public class FixedMultiColorBinderEntries
    {
        [SerializeField] public MultiColorBinderEntry[] colorBindings;

        /// <summary>
        /// Gets the number of entries in the array.
        /// </summary>
        public int Length => colorBindings?.Length ?? 0;

        /// <summary>
        /// Indexer to access individual entries.
        /// </summary>
        public MultiColorBinderEntry this[int index]
        {
            get => colorBindings[index];
            set => colorBindings[index] = value;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FixedMultiColorBinderEntries()
        {
            colorBindings = new MultiColorBinderEntry[0];
        }

        /// <summary>
        /// Constructor that initializes the array with a specific size.
        /// </summary>
        /// <param name="size">The number of entries to create.</param>
        public FixedMultiColorBinderEntries(int size)
        {
            colorBindings = new MultiColorBinderEntry[size];
            for (int i = 0; i < size; i++)
            {
                colorBindings[i] = new MultiColorBinderEntry();
            }
        }

        /// <summary>
        /// Constructor that initializes the array with existing entries.
        /// </summary>
        /// <param name="entries">The entries to wrap.</param>
        public FixedMultiColorBinderEntries(MultiColorBinderEntry[] entries)
        {
            this.colorBindings = entries ?? new MultiColorBinderEntry[0];
        }
    }
}
