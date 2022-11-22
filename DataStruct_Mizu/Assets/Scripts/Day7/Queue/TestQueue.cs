using System.Collections;
using UnityEngine;

namespace Mizu
{
    public class TestQueue : StudyBase
    {
        protected override void OnLog()
        {
            var queue = new Queue<string>();

            //queue.Peek(); // 예외 처리 확인용
            //queue.Dequeue(); // 예외 처리 확인용
            queue.Enqueue("1stJob");
            queue.Enqueue("2ndJob");
            // 2ndJob
            Log(queue.Peek());

            queue.Enqueue("3rdJob");
            var str = queue.Dequeue();
            // 1stJob;
            Log(str);

            queue.Enqueue("4thJob");
            // 2ndJob, 3rdJob, 4thJob
            queue.LogValues();

            //var q = new Queue<Object>();
            //Object obj = null;
            //q.Enqueue(obj);
        }
    }
}