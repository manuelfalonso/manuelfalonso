using System;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Represents a strategy that consists of multiple strategies to be executed sequentially.
    /// This class allows executing multiple strategies of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of data passed to each strategy.</typeparam>
    public abstract class MultiStrategySO<T> : StrategySO<T>
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The list of strategies to execute. Strategies must not be null.
        /// </summary>
        [Tooltip("The list of strategies to execute. Strategies must not be null.")]
        [SerializeField] protected StrategySO<T>[] _strategies = Array.Empty<StrategySO<T>>();

        public override bool CanExecute(T data)
        {
            if (_strategies == null || _strategies.Length == 0)
            {
                Debug.LogError("No strategies defined in MultiStrategySO!", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes all strategies in the array.
        /// </summary>
        /// <param name="data">The data passed to each strategy.</param>
        public override void Execute(T data)
        {            
            foreach (var strategy in _strategies)
            {
                try
                {
                    if (strategy.TryToExecute(data))
                    {
                        if (_showLogs)
                        {
                            Debug.Log($"Successfully executed strategy: {strategy.name}", this);
                        }
                    }
                    else
                    {
                        if (_showLogs)
                        {
                            Debug.LogWarning($"Failed to execute strategy: {strategy.name}", this);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }
    }

    /// <summary>
    /// Represents a strategy that consists of multiple parameterless strategies to be executed sequentially.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMultiStrategy", menuName = "Sombra Studios/Strategies/Multi Strategy")]
    public class MultiStrategySO : StrategySO
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The list of strategies to execute. Strategies must not be null.
        /// </summary>
        [Tooltip("The list of strategies to execute. Strategies must not be null.")]
        [SerializeField] protected StrategySO[] _strategies = Array.Empty<StrategySO>();

        public override bool CanExecute()
        {
            if (_strategies == null || _strategies.Length == 0)
            {
                Debug.LogError("No strategies defined in MultiStrategySO!", this);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes all strategies in the array.
        /// </summary>
        /// <param name="data">The data passed to each strategy.</param>
        public override void Execute()
        {
            foreach (var strategy in _strategies)
            {
                try
                {
                    if (strategy == this)
                    {
                        Debug.LogWarning("MultiStrategySO cannot contain itself as a strategy!", this);
                        continue;
                    }

                    if (strategy.TryToExecute())
                    {
                        if (_showLogs)
                        {
                            Debug.Log($"Successfully executed strategy: {strategy.name}", this);
                        }
                    }
                    else
                    {
                        if (_showLogs)
                        {
                            Debug.LogWarning($"Failed to execute strategy: {strategy.name}", this);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }
    }
}