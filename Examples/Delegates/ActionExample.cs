using UnityEngine;
using System;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Action is a void return value Delegate
    /// </summary>
    public class ActionExample : MonoBehaviour
    {
        // It's equal to "delegate void VoidNoParameterDelegate()"
        private Action _voidNoParameterAction;

        // It's equal to "delegate void VoidIntParameterDelegate(int i)"
        private Action<int> _voidIntegerParameterAction;

        // Start is called before the first frame update
        void Start()
        {
            TestVoidNoParameterAction();
            TestVoidIntegerParameterAction();
        }

        private void TestVoidNoParameterAction()
        {
            Debug.Log("====================================");
            Debug.Log("LAMBDA EXPRESSION WITH ACTION VOID DELEGATE");
            // Lambda expression with Action void delegate
            _voidNoParameterAction += () => { Debug.Log("Action delegate"); };
            _voidNoParameterAction();
        }

        private void TestVoidIntegerParameterAction()
        {
            Debug.Log("====================================");
            Debug.Log("LAMBDA EXPRESSION WITH ACTION INTEGER DELEGATE");
            _voidIntegerParameterAction +=
                (int number) => { Debug.Log("Action int delegate: " + number); };
            _voidIntegerParameterAction(5);
        }
    }
}
