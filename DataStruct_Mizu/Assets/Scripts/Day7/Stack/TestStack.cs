using System.Collections;
using UnityEngine;

namespace Mizu
{
    public class TestStack : StudyBase
    {
        protected override void OnLog()
        {
            var stack = new Stack<string>(1);
            stack.Push("Hello");
            // Hello
            Log(stack.Peek());

            stack.Push("Hi");
            var str = stack.Pop();
            // Hi
            Log(str);

            stack.Push("My name is : ");
            stack.Push("Mizu!");
            // Hello. My name is : Mizu!
            stack.LogValues();
        }
    }
}