using UnityEngine;

/// <summary>
/// Store a function in a variable with Delegate
/// </summary>
public class DelegateExample : MonoBehaviour
{
    // Delegate
    public delegate void VoidNoParameterDelegate();
    public delegate bool BoolIntParameterDelegate(int i);

    // Fields
    private VoidNoParameterDelegate myVoidNoParameterDelegate;
    private BoolIntParameterDelegate myBoolIntParameterDelegate;

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
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
        myVoidNoParameterDelegate = MyVoidNoParameterDelegateFunction;
        // Invoke delegate
        myVoidNoParameterDelegate();

        // Sign function
        myVoidNoParameterDelegate = MySecondVoidNoParameterDelegateFunction;
        // Invoke delegate
        myVoidNoParameterDelegate();

        // ---------

        // MULTICAST
        // Sign function
        myVoidNoParameterDelegate = MyVoidNoParameterDelegateFunction;
        myVoidNoParameterDelegate += MySecondVoidNoParameterDelegateFunction;
        // Invoke delegate
        myVoidNoParameterDelegate();
    }

    /// <summary>
    /// Test a boolean with int parameter delegate
    /// </summary>
    private void TestBoolIntParameterDelegate()
    {
        Debug.Log("===============================");
        Debug.Log("TEST BOOLEAN INTEGER PARAMETER DELEGATE");
        
        myBoolIntParameterDelegate += MyBoolIntDelegateFunction;
        myBoolIntParameterDelegate += MySecondBoolIntDelegateFunction;
        myBoolIntParameterDelegate(5);
    }

    /// <summary>
    /// Test creating a explicit delegate and with anonymous method
    /// </summary>
    private void ExplicitAndAnonymousCreateDelegate()
    {
        Debug.Log("===============================");
        Debug.Log("TEST EXPLICIT CREATE DELEGATE");
        
        // Explicit create delegate
        myVoidNoParameterDelegate = 
            new VoidNoParameterDelegate(MyVoidNoParameterDelegateFunction);
        // Anonymous method
        myVoidNoParameterDelegate = delegate () { Debug.Log("Anonymous method"); };
    }

    #endregion

    #region Secondary Methods

    /// <summary>
    /// Test a void with no parameter functions
    /// </summary>
    void MyVoidNoParameterDelegateFunction()
    {
        Debug.Log("MyVoidNoParameterDelegateFunction");
    }

    /// <summary>
    /// Test a void with no parameter functions
    /// </summary>
    void MySecondVoidNoParameterDelegateFunction()
    {
        Debug.Log("MySecondVoidNoParameterDelegateFunction");
    }

    /// <summary>
    /// Test a boolean with integer parameter function
    /// </summary>
    /// <param name="number">Number to check if its positive</param>
    /// <returns></returns>
    bool MyBoolIntDelegateFunction(int number)
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
    bool MySecondBoolIntDelegateFunction(int number)
    {
        bool result = number < 0;
        Debug.Log("MySecondBoolIntDelegateFunction return " + result);
        return result;
    }

    #endregion
}
