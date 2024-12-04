using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// An example script for using the Stat system.
    /// </summary>
    public class StatClientExample : MonoBehaviour
    {
        [SerializeField] private BoolStat _boolStat = new(false, "Bool Test");
        [SerializeField] private IntStat _intStat = new IntStat(0, "Int Test");
        [SerializeField] private FloatStat _floatStat = new(0f, "Float Test");

        [SerializeField] private StatSheetSO _statSheet;

        private StatContainer _statContainer;


        // Start is called before the first frame update
        private void Start()
        {
            BoolStatExample();
            IntStatExample();
            FloatStatExample();
            StatSheetExample();
        }


        private void BoolStatExample()
        {
            Debug.Log($"=== Bool Stat ===");
            // Stat modified event
            _boolStat.StatModified -= BoolStat_StatModified;
            _boolStat.StatModified += BoolStat_StatModified;
            // Create Stat modifier
            var boolModifier = new StatBoolModifier(true, this);
            // Add modifier
            _boolStat.AddModifier(boolModifier);
            // Debug source and value of Stat modifier
            Debug.Log($"Bool modifier source {boolModifier._source} - value {boolModifier.Value}");
            // Has modifier from this source
            Debug.Log($"Has active modifier from this source: {_boolStat.HasModifierFromSource(this)}");
            // Remove modifier
            _boolStat.RemoveModifier(boolModifier);
        }

        private void IntStatExample()
        {
            Debug.Log($"=== Int Stat ===");
            // Stat modified event
            _intStat.StatModified -= IntStat_StatModified;
            _intStat.StatModified += IntStat_StatModified;
            // Create additive stat modifier
            var additiveIntModifier =
                new StatIntModifier(10, ModifierOperationType.Additive, this);
            // Add additive Stat modifier
            _intStat.AddModifier(additiveIntModifier);
            // Debug source and value of Stat modifier
            Debug.Log($"Int additive modifier source {additiveIntModifier._source} - {additiveIntModifier.Value}");
            // Create multiplicative stat modifier
            var multiplicativeStatModifier =
                new StatIntModifier(20, ModifierOperationType.Multiplicative);
            // Add multiplicative Stat modifier
            _intStat.AddModifier(multiplicativeStatModifier);
            // Remove multiplicative Stat modifier
            _intStat.RemoveModifier(multiplicativeStatModifier);
            // Remove all modifiers from this source
            _intStat.RemoveModifiersFromSource(this);
        }

        private void FloatStatExample()
        {
            Debug.Log($"=== Float Stat ===");
            // Stat modified event
            _floatStat.StatModified -= FloatStat_StatModified;
            _floatStat.StatModified += FloatStat_StatModified;
            // Create additive stat modifier
            var additiveFloatModifier =
                new StatFloatModifier(10f, ModifierOperationType.Additive, this);
            // Add additive Stat modifier
            _floatStat.AddModifier(additiveFloatModifier);
            // Create multiplicative stat modifier
            var multiplicativeStatModifier =
                new StatFloatModifier(20f, ModifierOperationType.Multiplicative);
            // Add multiplicative Stat modifier
            _floatStat.AddModifier(multiplicativeStatModifier);
            // Remove multiplicative Stat modifier
            _floatStat.RemoveModifier(multiplicativeStatModifier);
            // Remove all modifiers from this source
            _floatStat.RemoveModifiersFromSource(this);
            // Debug source and value of Stat modifier
            Debug.Log($"Float additive modifier source {additiveFloatModifier._source} - {additiveFloatModifier.Value}");
        }

        private void StatSheetExample()
        {
            Debug.Log($"=== Stat Sheet ===");
            // Instead of using a reference, an instance can be used
            _statSheet = (StatSheetSO)ScriptableObject.CreateInstance(nameof(StatSheetSO));
            // Add Stats on runtime
            _statSheet.AddStat(_boolStat);
            _statSheet.AddStat(_intStat);
            // Remove Stats on runtime
            _statSheet.RemoveStat(_boolStat);
            // Create a new Stat Container
            _statContainer = new StatContainer(_statSheet);
            // Get a Stat
            if (!_statContainer.TryGetValue("Int Test", out IntStat intStat)) { return; }
            Debug.Log($"Get Stat {intStat.Name}: {intStat.GetValue()}");
        }

        /// <summary>
        /// Called when the boolean stat is modified.
        /// </summary>
        private void BoolStat_StatModified(bool newValue)
        {
            // Debug Stat name, initial value and modified value
            Debug.Log($"{_boolStat.Name}: Initial {_boolStat.GetBaseValue()} - Actual {newValue}");
        }

        /// <summary>
        /// Called when the integer stat is modified.
        /// </summary>
        private void IntStat_StatModified(int newValue)
        {
            // Debug Stat name, initial value and modified value
            Debug.Log($"{_intStat.Name}: Initial {_intStat.GetBaseValue()} - Actual {newValue}");
        }

        /// <summary>
        /// Called when the float stat is modified.
        /// </summary>
        private void FloatStat_StatModified(float newValue)
        {
            // Debug Stat name, initial value and modified value
            Debug.Log($"{_floatStat.Name}: Initial {_floatStat.GetBaseValue()} - Actual {newValue}");
        }
    }
}
