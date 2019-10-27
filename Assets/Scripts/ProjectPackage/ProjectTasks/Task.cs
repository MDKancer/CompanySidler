using System;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Human;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectPackage.ProjectTasks
{
    public class Task 
    {
        private static int taskIndex = 0;
        public TaskType taskType;
        private bool isDone = false;
        private float timeDuration;
        private List<BuildingWorkers<Worker, EntityType>> taskTakers;
        private (float percentDoneProgress, float howMuchNeed) progressBar = (0,100.0f);

        public Task()
        {
            var taskTypeValues =  Enum.GetValues(typeof(TaskType));
            var enumCount = taskTypeValues.Length;
            if (taskIndex == enumCount-1)
            {
                taskIndex = 0;
            }
            else
            {
                taskIndex++;
            }
            taskType = (TaskType) taskTypeValues.GetValue(taskIndex);
            timeDuration = Random.Range(5f, 20f);
            taskTakers = new List<BuildingWorkers<Worker, EntityType>>();
        }

        public TaskType TaskType => taskType;

        public bool IsDone => isDone;

        /// <summary>
        /// It can be Changeble, depend how many workers work on the Task
        /// </summary>
        public float TimeDuration
        {
            get => timeDuration;
            private  set => timeDuration = value;
        }

        public List<BuildingWorkers<Worker, EntityType>> TaskTakers => taskTakers;

        public void ApplyTaskTaker(Worker Worker)
        {
            if(progressBar.percentDoneProgress < progressBar.howMuchNeed)
            {
                var bw = new BuildingWorkers<Worker, EntityType>(Worker.WorkerData.GetEntityType)
                {
                    Worker = Worker
                };
                taskTakers.Add(bw);
                timeDuration /= taskTakers.Count;
            }
            
        }

        /// <summary>
        /// It can only change the 
        ///     <name>percentDoneProgress</name>
        /// </summary>
        public  (float percentDoneProgress, float howMuchNeed) ProgressBar
        {
            get => progressBar;
            set
            {
                progressBar = value;
                if (progressBar.percentDoneProgress >= progressBar.howMuchNeed) isDone = true;
            }
        }
    }
}