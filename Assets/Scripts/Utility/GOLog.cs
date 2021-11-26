using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class GOLog
{
    public static void Log()
    {
        Log(new StackTrace(), "");
    }

    public static void Log(string str)
    {
        Log(new StackTrace(), str);
    }

    private static void Log(StackTrace stackTrace, string str)
    {
        // var frame = stackTrace.GetFrame(1);
        var method = stackTrace.GetFrame(1).GetMethod();
        // Get calling method name
        UnityEngine.Debug.Log(method.DeclaringType.Name +"." + method.Name + " " + str);
    }

    // System.Environment.StackTrace
    // Debug.Log("" + MethodInfo.GetCurrentMethod().Name);


}
