using UnityEngine;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Lambda expresion with delegates.
    /// They can't be removed from delegates manually.
    /// </summary>
    public class LambdaExample : MonoBehaviour
    {
        // Delegate
        private delegate void VoidNoParameterDelegate();
        private delegate bool BoolIntParameterDelegate(int i);

        // Fields
        private VoidNoParameterDelegate _myVoidNoParameterDelegate;
        private BoolIntParameterDelegate _myBoolIntParameterDelegate;

        private void Start()
        {
            TestVoidNoParameterDelegate();
            TestBoolIntParameterDelegate();
        }

        private void TestVoidNoParameterDelegate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST VOID NO PARAMETER METHODS");

            // Lambda Expresion
            _myVoidNoParameterDelegate += () => { Debug.Log("Lambda expression"); };
            // Simplified method. Only when its with a single statement
            _myVoidNoParameterDelegate += () => Debug.Log("Single statement Lambda expression");
            // Can't be removed manually from the delegate
            _myVoidNoParameterDelegate();
        }

        private void TestBoolIntParameterDelegate()
        {
            Debug.Log("===============================");
            Debug.Log("TEST BOOLEAN INTEGER PARAMETER METHODS");
            
            _myBoolIntParameterDelegate += (int number) => {
                bool result = number < 0;
                Debug.Log("MyBoolIntDelegateFunction return " + result);
                return result;
            };
            _myBoolIntParameterDelegate += (int number) => {
                bool result = number > 0;
                Debug.Log("MyBoolIntDelegateFunction return " + result);
                return result;
            };
            _myBoolIntParameterDelegate(5);
        }
    }
}
