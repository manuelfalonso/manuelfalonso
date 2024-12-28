using UnityEngine;
using System;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Store a function in a variable with Predicate
    /// </summary>
    public class PredicateExample : MonoBehaviour
    {
        // Fields
        private Predicate<int> _isPositivePredicate;
        private Predicate<int> _isNegativePredicate;
        private Predicate<string> _isNonEmptyStringPredicate;

        #region Unity Methods

        private void Start()
        {
            TestIntegerPredicates();
            TestStringPredicate();
            TestAnonymousPredicate();
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// Test Predicates with integer checks
        /// </summary>
        private void TestIntegerPredicates()
        {
            Debug.Log("===============================");
            Debug.Log("TEST INTEGER PREDICATES");

            // Assign predicates
            _isPositivePredicate = IsPositive;
            _isNegativePredicate = IsNegative;

            // Test predicates
            Debug.Log($"Is 5 positive? {_isPositivePredicate(5)}"); // True
            Debug.Log($"Is -3 positive? {_isPositivePredicate(-3)}"); // False

            Debug.Log($"Is -7 negative? {_isNegativePredicate(-7)}"); // True
            Debug.Log($"Is 10 negative? {_isNegativePredicate(10)}"); // False
        }

        /// <summary>
        /// Test Predicate with a string check
        /// </summary>
        private void TestStringPredicate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST STRING PREDICATE");

            // Assign predicate
            _isNonEmptyStringPredicate = IsNonEmptyString;

            // Test predicate
            Debug.Log($"Is 'Hello' non-empty? {_isNonEmptyStringPredicate("Hello")}"); // True
            Debug.Log($"Is '' non-empty? {_isNonEmptyStringPredicate("")}"); // False
        }

        /// <summary>
        /// Test creating an anonymous Predicate
        /// </summary>
        private void TestAnonymousPredicate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST ANONYMOUS PREDICATE");

            // Create an anonymous Predicate
            Predicate<int> isEven = delegate (int number)
            {
                return number % 2 == 0;
            };

            // Test anonymous Predicate
            Debug.Log($"Is 4 even? {isEven(4)}"); // True
            Debug.Log($"Is 7 even? {isEven(7)}"); // False
        }

        #endregion

        #region Secondary Methods

        /// <summary>
        /// Checks if an integer is positive
        /// </summary>
        private bool IsPositive(int number)
        {
            return number > 0;
        }

        /// <summary>
        /// Checks if an integer is negative
        /// </summary>
        private bool IsNegative(int number)
        {
            return number < 0;
        }

        /// <summary>
        /// Checks if a string is non-empty
        /// </summary>
        private bool IsNonEmptyString(string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        #endregion
    }
}
