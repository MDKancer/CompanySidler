using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Enums;
using JetBrains.Annotations;
using ProjectPackage.ProjectTasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Employee
{
    public class Employee : Human, IWorker
    {

        private EmployeeData employeeData = null;
        private TextMeshProUGUI namePoster;

        public void OnEnable()
        {
            //TODO: das soll auch geändert werden, ist dumm gemacht.
            AttachEvent += SetOffice;
        }

        private void SetOffice(Building myOffice)
        {
            myOffice.applyWorkerEvent(this);
        }
        public EmployeeData EmployeeData
        {
            get => employeeData;
            set => employeeData = value;
        }
        public void Work()
        {
            StartCoroutine(DO());
            StartCoroutine(ShowMyCanvas());
        }
        
        protected float TaskDuration()
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
            if(EmployeeData.Project != null)
            {
                foreach (var task in EmployeeData.Project.Tasks)
                {
                    if (task.TaskTakers.Count == 0)
                    {
                        if (!task.IsDone) return task;
                    }
                }
                // TODO :  das soll später geändert werden,
                // TODO : um ein Task zurück geben, was mit seinem Beruf zutun hat.

                var tempTask = EmployeeData.Project.Tasks[Random.Range(0, EmployeeData.Project.Tasks.Count - 1)];
                return !tempTask.IsDone?  tempTask: null;
            }
            return null;
        }
        
        private IEnumerator DO()
        {
            int index = 0;
            List<HumanState> myKeys = EmployeeData.GetEntityWorkingCycle.Keys.ToList();
            Company company = employeeData.Company;
            Vector3 initialPosition = gameObject.transform.position;
            Vector3 officePosition =company.GetOffice(EmployeeData.GetHisOffice).gameObject.transform.position;
            Vector3 targetPosition = GenerateRandomPosition(officePosition);
            
            destination = EmployeeData.GetHisOffice;
            PathFinder.Navigator.MoveTo(gameObject,targetPosition);
            while (SelfState.CurrentState != HumanState.QUITED)
            {
                if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
                {
                    var taskDuration = TaskDuration();
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
                            
                            while (time <= taskDuration && SelfState.CurrentState != HumanState.QUITED)
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
                        // hier macht er seine Aufgabe.
                        yield return new WaitForSeconds(taskDuration);
                    }
                    
                    if(SelfState.CurrentState != HumanState.QUITED)
                    {
                        // hier stellt er sein neue Ziel
                        index = index >= myKeys.Count ? 0 : index;
                        
                        destination = EmployeeData.GetEntityWorkingCycle[myKeys[index]];
                        officePosition = company.GetOffice(destination).gameObject.transform.position;
                        
                        SelfState.CurrentState = myKeys[index];
                        
                        targetPosition = GenerateRandomPosition(officePosition);
                
                        PathFinder.Navigator.MoveTo(gameObject,targetPosition);
                        index++;
                    }
                }
                
                
                // hier passiert alles wärend des Laufens
                if (PathFinder.Navigator.MyPathStatus(gameObject) == PathProgress.NONE)
                {
                    targetPosition = GenerateRandomPosition(officePosition);
                }
                yield return null;
            }

            destination = BuildingType.NONE;
            targetPosition = initialPosition;
            PathFinder.Navigator.MoveTo(gameObject,targetPosition);

            while (SelfState.CurrentState == HumanState.QUITED)
            {
                if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
                {
                    Destroy(gameObject);
                }
                yield return null;
            }
        }

        private IEnumerator ShowMyCanvas()
        {
            namePoster = proceduralUiElements.GetCanvas(this.employeeData.GetEntityType.ToString());
            var main = Camera.main;
            RectTransform rectTransform = namePoster.GetComponent<RectTransform>();
            while (gameObject != null)
            {
                rectTransform.position =
                    gameObject.transform.position + (gameObject.transform.up * 3f);
                rectTransform.rotation = Quaternion.LookRotation(main.transform.forward);
                yield return null;
            }
        }

        private void OnDestroy()
        {
            Destroy(namePoster);
        }
    }
}