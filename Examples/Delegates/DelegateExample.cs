using UnityEngine;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Store a function in a variable with Delegate
    /// </summary>
    public class DelegateExample : MonoBehaviour
    {
        // Delegate
        private delegate void VoidNoParameterDelegate();
        private delegate bool BoolIntParameterDelegate(int i);

        // Fields
        private VoidNoParameterDelegate _myVoidNoParameterDelegate;
        private BoolIntParameterDelegate _myBoolIntParameterDelegate;

        #region Unity Methods

        // Start is called before the first frame update
        private void Start()
        {
            TestVoidNoParameterDelegate();
            TestBoolIntParameterDelegate();
            ExplicitAndAnonymousCreateDelegate();
        }

        #endregion

        #region Main Methods

        /// <summary>
        /// Test a void with no parameter delegate
        /// </summary>
        private void TestVoidNoParameterDelegate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST VOID NO PARAMETER DELEGATE");
        
            // SINGLE CAST
            // Sign function
            _myVoidNoParameterDelegate = MyVoidNoParameterDelegateFunction;
            // Invoke delegate
            _myVoidNoParameterDelegate();

            // Sign function
            _myVoidNoParameterDelegate = MySecondVoidNoParameterDelegateFunction;
            // Invoke delegate
            _myVoidNoParameterDelegate();

            // ---------

            // MULTICAST
            // Sign function
            _myVoidNoParameterDelegate = MyVoidNoParameterDelegateFunction;
            _myVoidNoParameterDelegate += MySecondVoidNoParameterDelegateFunction;
            // Invoke delegate
            _myVoidNoParameterDelegate();
        }

        /// <summary>
        /// Test a boolean with int parameter delegate
        /// </summary>
        private void TestBoolIntParameterDelegate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST BOOLEAN INTEGER PARAMETER DELEGATE");
        
            _myBoolIntParameterDelegate += MyBoolIntDelegateFunction;
            _myBoolIntParameterDelegate += MySecondBoolIntDelegateFunction;
            _myBoolIntParameterDelegate(5);
        }

        /// <summary>
        /// Test creating a explicit delegate and with anonymous method
        /// </summary>
        private void ExplicitAndAnonymousCreateDelegate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST EXPLICIT CREATE DELEGATE");
        
            // Explicit create delegate
            _myVoidNoParameterDelegate = 
                new VoidNoParameterDelegate(MyVoidNoParameterDelegateFunction);
            _myVoidNoParameterDelegate();
            // Anonymous method
            _myVoidNoParameterDelegate = delegate () { Debug.Log("Anonymous method"); };
            _myVoidNoParameterDelegate();
        }

        #endregion

        #region Secondary Methods

        /// <summary>
        /// Test a void with no parameter functions
        /// </summary>
        private void MyVoidNoParameterDelegateFunction()
        {
            Debug.Log("MyVoidNoParameterDelegateFunction");
        }

        /// <summary>
        /// Test a void with no parameter functions
        /// </summary>
        private void MySecondVoidNoParameterDelegateFunction()
        {
            Debug.Log("MySecondVoidNoParameterDelegateFunction");
        }

        /// <summary>
        /// Test a boolean with integer parameter function
        /// </summary>
        /// <param name="number">Number to check if its positive</param>
        /// <returns></returns>
        private bool MyBoolIntDelegateFunction(int number)
        {
            bool result = number > 0;
            Debug.Log("MyBoolIntDelegateFunction return " + result);
            return result;
        }

        /// <summary>
        /// Test a boolean with integer parameter function
        /// </summary>
        /// <param name="number">Number to check if its negative</param>
        /// <returns></returns>
        private bool MySecondBoolIntDelegateFunction(int number)
        {
            bool result = number < 0;
            Debug.Log("MySecondBoolIntDelegateFunction return " + result);
            return result;
        }

        #endregion
    }
}
