using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a container for stats.
    /// </summary>
    [System.Serializable]
    public class StatContainer
    {
        /// <summary>
        /// Dictionary of stats.
        /// </summary>
        private readonly Dictionary<string, IBaseStat> _stats = new Dictionary<string, IBaseStat>();
        /// <summary>
        /// The stat sheet associated with this container.
        /// </summary>
        private StatSheetSO _myStatSheet;

        /// <summary>
        /// Gets the dictionary of stats.
        /// </summary>
        public Dictionary<string, IBaseStat> Stats => _stats;


        /// <summary>
        /// Constructs a new stat container with a stat sheet.
        /// </summary>
        /// <param name="statSheet">The stat sheet to use for initializing the container.</param>
        public StatContainer(StatSheetSO statSheet)
        {
            Initialize(statSheet);
        }


        /// <summary>
        /// Tries to get the value of a stat.
        /// </summary>
        /// <param name="statName">The name of the stat.</param>
        /// <param name="value">The value of the stat, if found.</param>
        /// <returns>True if the stat was found, false otherwise.</returns>
        public bool TryGetValue<T>(string statName, out T value)
        {
            value = default;

            if (_stats.TryGetValue(statName, out var stat))
            {
                if (stat is T t)
                {
                    value = t;
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Initializes the stat container with a stat sheet.
        /// </summary>
        /// <param name="statSheet">The stat sheet to use for initializing the container.</param>
        private void Initialize(StatSheetSO statSheet)
        {
            _myStatSheet = ScriptableObject.Instantiate(statSheet);
            foreach (var stat in _myStatSheet.Stats)
            {
                IBaseStat newStat = StatFactory.CreateStat(stat);
                _stats.Add(newStat.Name, newStat);
            }
        }
    }
}
//EOF.