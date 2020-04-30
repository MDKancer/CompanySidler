using System;
using System.Collections.Generic;
using Buildings;
using Entity.Employee;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectPackage.ProjectTasks
{
    public class Activity 
    {
        private static int taskIndex = 0;
        public TaskType taskType;
        private bool isDone = false;
        private float timeDuration;
        private List<BuildingWorkers<Employee, EntityType>> taskTakers;
        private (float percentDoneProgress, float howMuchNeed) progressBar = (0,100.0f);

        public Activity()
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
            taskTakers = new List<BuildingWorkers<Employee, EntityType>>();
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

        public List<BuildingWorkers<Employee, EntityType>> TaskTakers => taskTakers;

        public void ApplyTaskTaker(Employee employee)
        {
            if(progressBar.percentDoneProgress < progressBar.howMuchNeed)
            {
                var bw = new BuildingWorkers<Employee, EntityType>(employee.EmployeeData.GetEntityType)
                {
                    Worker = employee
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