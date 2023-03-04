using UnityEngine;
using System;

/// <summary>
/// Func is a return parameter Delegate
/// </summary>
public class FuncExample : MonoBehaviour
{
    // It's equal to "delegate bool BoolNoParameterDelegate()"
    private Func<bool> boolNoParameterFunc;

    // It's equal to "delegate bool BoolIntParameterDelegate(int i)";
    private Func<int, bool> boolIntegerParameterFunc;

    // Start is called before the first frame update
    void Start()
    {
        boolNoParameterFunc = () => false;
        Debug.Log(boolNoParameterFunc());

        // Func no acepta single statement method sin {}
        boolIntegerParameterFunc = (int numero) => { return numero > 0; };
        Debug.Log(boolIntegerParameterFunc(5));
    }
}
