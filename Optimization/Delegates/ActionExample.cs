using UnityEngine;
using System;

/// <summary>
/// Action is a void return value Delegate
/// </summary>
public class ActionExample : MonoBehaviour
{
    // It's equal to "delegate void VoidNoParameterDelegate()"
    private Action voidNoParameterAction;

    // It's equal to "delegate void VoidIntParameterDelegate(int i)"
    private Action<int> voidIntegerParameterAction;

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
        voidNoParameterAction += () => { Debug.Log("Action delegate"); };
        voidNoParameterAction();
    }

    private void TestVoidIntegerParameterAction()
    {
        Debug.Log("====================================");
        Debug.Log("LAMBDA EXPRESSION WITH ACTION INTEGER DELEGATE");
        voidIntegerParameterAction +=
            (int number) => { Debug.Log("Action inte delegate: " + number); };
        voidIntegerParameterAction(5);
    }
}
