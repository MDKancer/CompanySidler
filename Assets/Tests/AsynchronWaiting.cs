using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Tests
{
    public class AsynchronWaiting : MonoBehaviour
    {
        private Task task;
        private TaskAwaiter awaiter;
        private void Start()
        {

            StartCoroutine(Prozess());
        }

        private IEnumerator Prozess()
        {
            task = WaitOfDone();
            awaiter = task.GetAwaiter();
            do
            {
                Debug.Log(awaiter.IsCompleted);
                yield return null;
            } while (!awaiter.IsCompleted);
            Debug.Log(awaiter.IsCompleted);
        }

        private async Task WaitOfDone()
        {
            float duration = 2f;
            float time = 0f;
            while (time <= duration)
            {
                time += Time.deltaTime;
                await Task.Delay((int) (Time.deltaTime * 1000f));
            }
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}