using System;
using System.Collections;
using UnityEngine;

namespace Ticket.NPC
{
    /// <summary>
    /// Управляет НИПом по заданному расписанию. Расписание представляет из себя <see cref="GameObject"/>, к которому
    /// прекрепили набор <see cref="Task"/>-ов. Данный скрипт в <see cref="Start"/> соберёт эти задания и будет по очереди
    /// выполнять на них методы <see cref="Task.DoTask"/>, ожидая конца выполнения каждого задания с помощью события
    /// <see cref="Task.TaskComplete"/>.
    /// <see cref="NPCSchedule"/> и <see cref="Task"/>-и должны висеть на одном <see cref="GameObject"/>.
    /// </summary>
    [RequireComponent(typeof(Task))]
    public class NPCSchedule : MonoBehaviour
    {
        Task[] tasks;
        int i = 0;
        bool readyForNextTask = true;

        void Start()
        {
            tasks = GetComponents<Task>();
            StartCoroutine(DoTasks());
        }

        IEnumerator DoTasks()
        {
            while (true)
            {
                yield return new WaitUntil(() => readyForNextTask);

                readyForNextTask = false;
                tasks[i].TaskComplete += OnTaskComplete;
                tasks[i].DoTask();
            }
        }

        void OnTaskComplete()
        {
            tasks[i].TaskComplete -= OnTaskComplete;
            i = ++i % tasks.Length;
            readyForNextTask = true;
        }
    }

    public abstract class Task : MonoBehaviour
    {
        abstract public Action TaskComplete { get; set; }
        abstract public void DoTask();
    }
}
