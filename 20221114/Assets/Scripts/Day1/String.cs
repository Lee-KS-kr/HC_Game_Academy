using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    private void Start()
    {
        DoTest();
    }

    private void DoTest()
    {
        string str1 = "Hello World!";
        string str2 = "Hello World";
        Debug.Log($"{Object.ReferenceEquals(str1, str2)}");

        string str3 = str2 + "!";
        Debug.Log($"{Object.ReferenceEquals(str1, str3)}");
        Debug.Log($"{Object.ReferenceEquals(str2, str3)}");
    }
}
