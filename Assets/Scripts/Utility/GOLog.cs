using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class GOLog
{
    private static List<int> types = new List<int>();

    public static void ActivateClass(System.Type t)
    {
        types.Add(t.GetHashCode());
    }

    public static void Log()
    {
       
        Log(new StackTrace(), "");
    }

    public static void Log(string str)
    {
        Log(new StackTrace(), str);
    }

    public static void Log(bool condition, string str)
    {
        if(condition)
        {
            Log2(new StackTrace(), str);
        }
    }

    private static void Log(StackTrace stackTrace, string str)
    {
        // var frame = stackTrace.GetFrame(1);
        var method = stackTrace.GetFrame(1).GetMethod(); // Get calling method name

        if (types.Contains(method.DeclaringType.GetHashCode()))
        {
            UnityEngine.Debug.Log(method.DeclaringType.Name +"." + method.Name + " " + str);
        }
    }

    private static void Log2(StackTrace stackTrace, string str)
    {
        // var frame = stackTrace.GetFrame(1);
        var method = stackTrace.GetFrame(1).GetMethod(); // Get calling method name

        UnityEngine.Debug.Log(method.DeclaringType.Name + "." + method.Name + " " + str);
        
    }

    // System.Environment.StackTrace
    // Debug.Log("" + MethodInfo.GetCurrentMethod().Name);


}
