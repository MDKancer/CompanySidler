using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using ProjectPackage.ProjectTasks;
using UnityEngine;

namespace ProjectPackage
{
    public class Project
    {
        public CustomerType customerType;
        public float percentprocessBar = 0f;
        private int budget;
        private float timeDuration;
        private int punishment;
        private int wastagePercent = 10;
        private bool isDone = false;
        private List<Activity> tasks;

        public Project(int workersCount,MonoBehaviour monoBehaviour)
        {
            tasks = new List<Activity>(workersCount);
            GenerateTasks(workersCount);
            
            timeDuration += tasks.Sum(task => task.TimeDuration);

            var totalHourlyWage = 10 * workersCount;
            
            budget = workersCount * totalHourlyWage * (int) timeDuration;
            punishment  = budget * wastagePercent / 100;
            
            monoBehaviour.StartCoroutine(CheckIfIsDone());
        }

        public int Budget => budget;
        
        public float TimeDuration => timeDuration;

        public int Punishment => punishment;

        public bool IsDone => isDone;


        public List<Activity> Tasks => tasks;

        private void GenerateTasks(int taskCount)
        {
            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(new Activity());
            }
        }

        private IEnumerator CheckIfIsDone()
        {
            bool[] tasksStatus = new bool[tasks.Count];
            float sumAllProcess = 0f;
            while (!isDone)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasksStatus[i] = tasks[i].IsDone;

                    sumAllProcess += tasks[i].ProgressBar.percentDoneProgress;
                }

                percentprocessBar = sumAllProcess / tasks.Count;
                sumAllProcess = 0f;
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