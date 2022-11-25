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

            stack.Push("I");
            stack.Push("Luv");
            stack.Push("U");
            // U, Luv, I, Hello
            stack.LogValues();
        }
    }
}