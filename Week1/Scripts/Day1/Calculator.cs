using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

public class Calculator : MonoBehaviour
{
    private  StringBuilder str1 = new StringBuilder();
    private  StringBuilder str2 = new StringBuilder();
    private  StringBuilder str3 = new StringBuilder();

     float num1 = 13.5f;
     float num2 = -27.125f;
     float num3 = 0.125f;
     float num4 = -0.75f;

     void Start()
    {
        Devide(num1);
        Devide(num2);
        Devide(num3);
        Devide(num4);
    }

    private  void Devide(float num)
    {
        str1.Clear();
        str2.Clear();
        str3.Clear();

        Debug.Log($"숫자 : {num}");
        int temp = (int)num;
        num -= temp;

        if (num > 0)
            str1.Append("0");
        else
        
            str1.Append("1");
        
        temp = ToBinary(temp);
        int exp = (temp == 0) ? str2.Length - 1 : 0;

        int get = ToBinaryFloat(Math.Abs(num));

        string str = str2.ToString();
        str.Reverse();
        str += str3.ToString();

        string expo = Expend(str, exp);
        str1.Append(expo);
        str1.Append(str);

        for (int i = str1.Length; i < 32; i++)
            str1.Append("0");

        Debug.Log($"이진수 : {str1}");
    }

    private  int ToBinary(int num)
    {
        if (num == 0) return 0;

        if (num < 0) num = Mathf.Abs(num);

        if (num / 2 < 2)
        {
            str2.Append((num % 2).ToString());
            str2.Append((num / 2).ToString());
            return num / 2;
        }
        else
        {
            str2.Append((num % 2).ToString());
            return ToBinary(num / 2);
        }
    }

    private  int ToBinaryFloat(float num)
    {
        if (num < 0) num = Mathf.Abs(num);

        num *= 2;
        int answer = (int)num;
        str3.Append(answer.ToString());
        num -= answer;
        if (num != 0)
            return ToBinaryFloat(num);

        return answer;
    }

    private  string Expend(string str, int expo = 0)
    {
        str2.Clear();
        int count = expo;

        if (expo == 0)
        {
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                count--;
                if (c[i] == '1') break;
            }
        }

        count += 127;
        count = ToBinary(count);
        string answer = str2.ToString();
        answer.Reverse();
        return answer;
    }
}
