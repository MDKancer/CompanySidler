using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BootManager;
using BuildingPackage;
using Constants;
using InputManager;
using JetBrains.Annotations;
using PathFinderManager;
using ProjectPackage;
using ProjectPackage.ProjectTasks;
using UnityEngine;

namespace Human
{
    public class Worker : Human, IWorker
    {
        private WorkerData workerData = null;
        
        
        
        public WorkerData WorkerData
        {
            get => workerData;
            set => workerData = value;
        }
        private IEnumerator DO()
        {
            int index = 0;
            List<HumanState> myKeys = WorkerData.GetEntityWorkingCycle.Keys.ToList();
            List<Company> firmas = Boot.container.Firmas;
            Vector3 initialPosition = gameObject.transform.position;
            Vector3 officePosition =firmas[0].GetOffice(WorkerData.GetHisOffice).gameObject.transform.position;
            Vector3 targetPosition = GenerateRandomPosition(officePosition);
            
            destination = WorkerData.GetHisOffice;
            PathFinder.MoveTo(gameObject,targetPosition);
            while (SelfState.CurrentState != HumanState.QUITED)
            {
                if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
                {
                    var taskDuration = TimeToDo();
                    if(SelfState.CurrentState == HumanState.WORK)
                    {
                        var task = GetTask();
                        if(task != null)
                        {
                            task.ApplyTaskTaker(this);

                            taskDuration = task.TimeDuration;
                            var taskProgressBar = task.ProgressBar;
                            
                            var time = 0f;
                            var smoothDeltaTime = Time.smoothDeltaTime;
                            var part = taskProgressBar.howMuchNeed / (taskDuration / smoothDeltaTime);
                            
                            while (time <= taskDuration)
                            {
                                time += smoothDeltaTime;
                                
                                taskProgressBar.percentDoneProgress += part;
                                task.ProgressBar = taskProgressBar;
                                //Debug.Log("time : "+time+ " Total Time : " + taskDuration + " Progress : "+ taskProgressBar.percentDoneProgress);
                                yield return null;
                            }
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(taskDuration);
                    }
                    index = index >= myKeys.Count ? 0 : index;
                    
                    destination = WorkerData.GetEntityWorkingCycle[myKeys[index]];
                    officePosition = firmas[0].GetOffice(destination).gameObject.transform.position;
                    
                    SelfState.CurrentState = myKeys[index];
                    
                    targetPosition = GenerateRandomPosition(officePosition);
            
                    PathFinder.MoveTo(gameObject,targetPosition);
                    index++;
                }
                yield return null;
            }
            PathFinder.MoveTo(gameObject,initialPosition);
            yield return null;
        }

        public void Work()
        {
            Building.ApplyWorker(this);
            StartCoroutine(DO());
        }
        
        protected float TimeToDo()
        {
            if (SelfState.CurrentState == HumanState.WALK)
            {
                return Random.Range(0.2f,1f);
            }
            else if (SelfState.CurrentState == HumanState.WORK)
            {
                return Random.Range(5f,20f);
            }
            else if (SelfState.CurrentState == HumanState.LEARN)
            {
                return Random.Range(5f,15f);
            }
            else if (SelfState.CurrentState == HumanState.PAUSE)
            {
                return Random.Range(2f,15f);
            }
            else if (SelfState.CurrentState == HumanState.TALK)
            {
                return Random.Range(0.3f,5f);
            }
            else if (SelfState.CurrentState == HumanState.NONE)
            {
                return Random.Range(0.3f,5f);
            }

            return 0f;
        }
        
        [CanBeNull]
        protected Task GetTask()
        {
            if(WorkerData.Project != null)
            {
                foreach (var task in WorkerData.Project.Tasks)
                {
                    if (task.TaskTakers.Count == 0)
                    {
                        if (!task.IsDone) return task;
                    }
                }
                // TODO :  das soll später geändert werden,
                // TODO : um ein Task zurück geben, was mit seinem Beruf zutun hat.

                var tempTask = WorkerData.Project.Tasks[Random.Range(0, WorkerData.Project.Tasks.Count - 1)];
                return !tempTask.IsDone?  tempTask: null;
            }
            return null;
        }
        
        private iBuilding Building
        {
            get => InputController.FocusedBuilding?.GetComponent(typeof(iBuilding)) as iBuilding;
        }
    }
}