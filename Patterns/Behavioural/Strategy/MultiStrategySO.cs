using System;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    public abstract class MultiStrategySO<T> : StrategySO<T>
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// The list of strategies to execute. Strategies must not be null.
        /// </summary>
        [Tooltip("The list of strategies to execute. Strategies must not be null.")]
        [SerializeField]
        protected StrategySO<T>[] _strategies = Array.Empty<StrategySO<T>>();

        /// <inheritdoc/>
        /// <returns>True if all the strategies were executed successfully, otherwise false.</returns>
        public override bool TryToExecute(T data)
        {
            if (!ValidateStrategies()) return false;

            bool success = false;

            foreach (var strategy in _strategies)
            {
                try
                {
                    if (strategy.TryToExecute(data))
                    {
                        success = true;
                        if (_showLogs)
                        {
                            Debug.Log($"Successfully executed strategy: {strategy.name}", this);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error executing strategy '{strategy.name}': {e.Message}", this);
                }
            }

            return success;
        }

        /// <summary>
        /// Executes all strategies in the array.
        /// </summary>
        /// <param name="data">The data passed to each strategy.</param>
        public override void Execute(T data)
        {
            if (!ValidateStrategies()) return;

            foreach (var strategy in _strategies)
            {
                try
                {
                    strategy.Execute(data);
                    if (_showLogs)
                    {
                        Debug.Log($"Executed strategy: {strategy.name}", this);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error executing strategy '{strategy.name}': {e.Message}", this);
                }
            }
        }

        /// <summary>
        /// Validates the `_strategies` field and logs errors if misconfigured.
        /// </summary>
        /// <returns>True if the array is valid, otherwise false.</returns>
        private bool ValidateStrategies()
        {
            if (_strategies == null || _strategies.Length == 0)
            {
                Debug.LogError("No strategies defined in MultiStrategySO!", this);
                return false;
            }

            return true;
        }
    }
}