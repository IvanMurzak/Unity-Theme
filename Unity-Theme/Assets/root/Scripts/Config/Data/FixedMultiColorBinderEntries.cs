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
        [SerializeField] private MultiColorBinderEntry[] entries;

        /// <summary>
        /// Gets the array of color entries.
        /// </summary>
        public MultiColorBinderEntry[] Entries
        {
            get => entries;
            set => entries = value;
        }

        /// <summary>
        /// Gets the number of entries in the array.
        /// </summary>
        public int Length => entries?.Length ?? 0;

        /// <summary>
        /// Indexer to access individual entries.
        /// </summary>
        public MultiColorBinderEntry this[int index]
        {
            get => entries[index];
            set => entries[index] = value;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FixedMultiColorBinderEntries()
        {
            entries = new MultiColorBinderEntry[0];
        }

        /// <summary>
        /// Constructor that initializes the array with a specific size.
        /// </summary>
        /// <param name="size">The number of entries to create.</param>
        public FixedMultiColorBinderEntries(int size)
        {
            entries = new MultiColorBinderEntry[size];
            for (int i = 0; i < size; i++)
            {
                entries[i] = new MultiColorBinderEntry();
            }
        }

        /// <summary>
        /// Constructor that initializes the array with existing entries.
        /// </summary>
        /// <param name="entries">The entries to wrap.</param>
        public FixedMultiColorBinderEntries(MultiColorBinderEntry[] entries)
        {
            this.entries = entries ?? new MultiColorBinderEntry[0];
        }
    }
}
