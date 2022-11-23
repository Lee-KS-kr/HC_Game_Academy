using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day5 : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _sprite;

    private void Start()
    {
        //PrintQ1();
        PrintQ2();
    }

    #region Q1
    private int CalcSomeCount<T>(T input) where T : IConvertible
    {
        var temp = string.Concat(input);
        decimal get = 0;
        if (!Decimal.TryParse(temp, out get))
        {
            throw new Exception($"Exception: 지원하지 않는 형식 입니다. String");
        }


        if (get < 0)
            get *= -1;
        int count = 0;

        while (1 < get)
        {
            ++count;
            get /= 2;
        }

        return count;
    }

    public void PrintQ1()
    {
        Debug.Log($"double\t:\t{CalcSomeCount<double>(10.0)}");
        Debug.Log($"float\t\t:\t{CalcSomeCount<float>(-20.0f)}");
        Debug.Log($"int\t\t:\t{CalcSomeCount<int>(13)}");
        Debug.Log($"string\t\t:\t{CalcSomeCount<string>("-123")}");
        Debug.Log($"string\t\t:\t{CalcSomeCount<string>("asdf")}");

        // 출력 결과
        // double : 4
        // float  : 5
        // int    : 4
        // string : 7
        // Exception: 지원하지 않는 형식 입니다. String
    }
    #endregion Q1

    #region Q2
    private void InitImage<T>(T t, Sprite sprite, Action<T, Sprite> action)
        where T : Graphic
    {
        if (null == sprite)
        {
            t.gameObject.SetActive(false);
            return;
        }

        //t.sprite = u; // 이걸 어떻게 구현할 것인가
        action?.Invoke(t, sprite);
        t.SetNativeSize();

        RectTransform rtf = t.GetComponent<RectTransform>();
        rtf.anchoredPosition = Vector2.zero;
    }

    private void PrintQ2()
    {
        InitImage<Image>(_image, _sprite, (image, sprite) => image.sprite = sprite);
        InitImage<RawImage>(_rawImage, _sprite, (rawImage, sprite) => rawImage.texture = sprite.texture);
    }

    // 얘를 델리게이트로 쓴다??
    private void ChangeSprite<T>(T theType) where T : Graphic
    {
        switch(theType)
        {
            case Image:
                break;
            case RawImage:
                break;
            default:
                break;
        }
    }
    #endregion
}

class Old
{
    #region Old_Q1
    private int CalcSomeCount_Old(int number)
    {
        if (number < 0)
            number *= -1;

        int count = 0;

        while (1 < number)
        {
            ++count;
            number /= 2;
        }

        return count;
    }


    private void Print()
    {
        //Debug.Log($"double : {CalcSomeCount<double>(10.0)}");
        //Debug.Log($"float  : {CalcSomeCount<float>(-20.0f)}");
        //Debug.Log($"int    : {CalcSomeCount<int>(13)}");
        //Debug.Log($"string : {CalcSomeCount<string>("-123")}");
        //Debug.Log($"string : {CalcSomeCount<string>("asdf")}");

        // 출력 결과
        // double : 4
        // float  : 5
        // int    : 4
        // string : 7
        // Exception: 지원하지 않는 형식 입니다. String
    }
    #endregion Old_Q1

    #region Old_Q2
    private void InitImage(Image image, Sprite sprite)
    {
        if (null == sprite)
        {
            image.gameObject.SetActive(false);
            return;
        }

        image.sprite = sprite;
        image.SetNativeSize();

        RectTransform rtf = image.GetComponent<RectTransform>();
        rtf.anchoredPosition = Vector2.zero;
    }

    private void InitImage(RawImage image, Texture texture)
    {
        if (null == texture)
        {
            image.gameObject.SetActive(false);
            return;
        }

        image.SetNativeSize();
        image.texture = texture;

        RectTransform rtf = image.GetComponent<RectTransform>();
        rtf.anchoredPosition = Vector2.zero;
    }

    private void Old_PrintQ2()
    {
        //[SerializeField] private RawImage _rawImage;
        //[SerializeField] private Image _image;

        //[SerializeField] private Sprite _sprite;

        //private void Start()
        //{
        //    InitImage<RawImage>( ??? );
        //    InitImage<Image>( ??? );
        //}
    }
    #endregion Old_Q2
}
