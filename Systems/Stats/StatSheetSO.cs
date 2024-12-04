using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a collection of stats.
    /// </summary>
    [CreateAssetMenu(fileName = "New Stat Sheet", menuName = "Sombra Studios/Stat Sheet")]
    public class StatSheetSO : ScriptableObject
    {
        /// <summary>
        /// List of float stats.
        /// </summary>
        [SerializeField] private List<FloatStat> _floatStats = new List<FloatStat>();
        /// <summary>
        /// List of integer stats.
        /// </summary>
        [SerializeField] private List<IntStat> _intStats = new List<IntStat>();
        /// <summary>
        /// List of boolean stats.
        /// </summary>
        [SerializeField] private List<BoolStat> _boolStats = new List<BoolStat>();

        /// <summary>
        /// Gets a list of all stats.
        /// </summary>
        public List<IBaseStat> Stats => _floatStats.Cast<IBaseStat>()
            .Concat(_intStats.Cast<IBaseStat>())
            .Concat(_boolStats.Cast<IBaseStat>())
            .ToList();


        /// <summary>
        /// Adds a stat to the appropriate list.
        /// </summary>
        /// <param name="stat">The stat to add.</param>
        public void AddStat(IBaseStat stat)
        {
            switch (stat)
            {
                case FloatStat floatStat:
                    _floatStats.Add(floatStat);
                    break;
                case IntStat intStat:
                    _intStats.Add(intStat);
                    break;
                case BoolStat boolStat:
                    _boolStats.Add(boolStat);
                    break;
            }
        }

        /// <summary>
        /// Removes a stat from the appropriate list.
        /// </summary>
        /// <param name="stat">The stat to remove.</param>
        public void RemoveStat(IBaseStat stat)
        {
            switch (stat)
            {
                case FloatStat floatStat:
                    _floatStats.Remove(floatStat);
                    break;
                case IntStat intStat:
                    _intStats.Remove(intStat);
                    break;
                case BoolStat boolStat:
                    _boolStats.Remove(boolStat);
                    break;
            }
        }
    }
}
//EOF.