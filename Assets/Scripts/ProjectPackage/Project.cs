using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BootManager;
using Enums;
using ProjectPackage.ProjectTasks;
using UnityEngine;

namespace ProjectPackage
{
    public class Project
    {
        public ClientType clientType;
        private int budget;
        private float timeDuration;
        private int punishment;
        private int wastagePercent = 10;
        private bool isDone = false;
        private List<Task> tasks;

        public Project(int workersCount)
        {
            tasks = new List<Task>(workersCount);
            GenerateTasks(workersCount);
            
            timeDuration += tasks.Sum(task => task.TimeDuration);

            var totalHourlyWage = 10 * workersCount;
            
            budget = workersCount * totalHourlyWage * (int)timeDuration;
            punishment  = budget * wastagePercent / 100;
            
            Boot.monobehaviour.StartCoroutine(CheckIfIsDone());
        }

        public int Budget => budget;
        
        public float TimeDuration => timeDuration;

        public int Punishment => punishment;

        public bool IsDone => isDone;


        public List<Task> Tasks => tasks;

        private void GenerateTasks(int taskCount)
        {
            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(new Task());
            }
        }

        private IEnumerator CheckIfIsDone()
        {
            bool[] tasksStatus = new bool[tasks.Count];
            while (!isDone)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasksStatus[i] = tasks[i].IsDone;
                }

                if (!tasksStatus.Contains(false))
                {
                    isDone = true;
                    RemoveProject();
                } 
                yield return null;
            }
        }

        private void RemoveProject()
        {
            foreach (var task in Tasks)
            {
                foreach (var taskTaker in task.TaskTakers)
                {
                    taskTaker.Worker.EmployeeData.Project = null;
                }
            }
        }
    }
}