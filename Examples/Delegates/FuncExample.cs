using UnityEngine;
using System;

namespace SombraStudios.Shared.Examples.Delegates
{
    /// <summary>
    /// Func is a return parameter Delegate
    /// </summary>
    public class FuncExample : MonoBehaviour
    {
        // It's equal to "delegate bool BoolNoParameterDelegate()"
        private Func<bool> _boolNoParameterFunc;

        // It's equal to "delegate bool BoolIntParameterDelegate(int i)";
        private Func<int, bool> _boolIntegerParameterFunc;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("====================================");
            Debug.Log("LAMBDA EXPRESSION WITH FUNC BOOL");
            _boolNoParameterFunc = () => false;
            Debug.Log(_boolNoParameterFunc());

            Debug.Log("LAMBDA EXPRESSION WITH FUNC INT BOOL");
            _boolIntegerParameterFunc = (int number) => number > 0;
            Debug.Log(_boolIntegerParameterFunc(5));
        }
    }
}
