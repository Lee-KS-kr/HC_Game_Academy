using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class NewCalc : MonoBehaviour
{
    private StringBuilder str1 = new StringBuilder();
    private StringBuilder str2 = new StringBuilder();
    private StringBuilder str3 = new StringBuilder();

    float num1 = 13.5f;
    float num2 = -27.125f;
    float num3 = 0.125f;
    float num4 = -0.75f;

    private void Start()
    {
        Devide(num1);
        Devide(num2);
        Devide(num3);
        Devide(num4);
    }

    private void Devide(float num)
    {
        str1.Clear();
        str2.Clear();
        str3.Clear();

        num = GetSign(num);
        int intNum = (int)num;

        num -= intNum;
        GetFraction(intNum);
        GetUnderDot(num);

        string get = str2.ToString();
        get = Reverse(get);
        int exp = (intNum == 0) ? get.Length - 1 : 0;
        get += str3.ToString();

        string exponent = GetExponent(get, exp);
        str1.Append(exponent);
        str1.Append(get);

        Debug.Log($"{str1}");
    }

    private float GetSign(float num)
    {
        string sign = (num > 0) ? "0" : "1";
        str1.Append(sign);

        return Mathf.Abs(num);
    }

    private int GetFraction(int num)
    {
        if (num == 0) return 0;

        if (num / 2 < 2)
        {
            str2.Append((num % 2).ToString());
            str2.Append((num / 2).ToString());
            return num / 2;
        }
        else
        {
            str2.Append((num % 2).ToString());
            return GetFraction(num / 2);
        }
    }

    private int GetUnderDot(float num)
    {
        num *= 2;
        int answer = (int)num;
        str3.Append(answer.ToString());
        num -= answer;
        if (num != 0)
            return GetUnderDot(num);

        return answer;
    }

    private string GetExponent(string input, int exp)
    {
        str2.Clear();
        int count = exp;

        if (exp == 0)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                count--;
                if (c[i] == '1') break;
            }
        }

        count += 127;
        count = GetFraction(count);
        string answer = str2.ToString();
        answer = Reverse(answer);
        return answer;
    }

    private string Reverse(string str)
    {
        string answer = "";
        char[] c = str.ToCharArray();

        for(int i=c.Length-1;i>=0;i--)
        {
            answer += c[i].ToString();
            if (c[0] == '0') break;
        }

        return answer;
    }
}
