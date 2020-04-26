using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Enums;
using JetBrains.Annotations;
using PathFinder;
using ProjectPackage.ProjectTasks;
using StateManager;
using StateManager.States.EmploeeStates;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Entity.Employee
{
    public class Employee : Human, IWorker
    {
        private Activity chore;
        private float activityDuration;
        private EmployeeData employeeData;
        private TextMeshProUGUI namePoster;
        private List<HumanState> employeeStates;
        private Task task;
        private TaskAwaiter awaiter;
        
        //_____
        private StateMachineClass<EmployeeState> stateMachineClass;

        private Dictionary<HumanState,EmployeeState> states;
        //_____
        private void RegisterAn()
        {
            //stateMachineClass = new StateMachineClass<EmployeeState>();
            
            states = new Dictionary<HumanState,EmployeeState>();
            states.Add(HumanState.WORK,new Work());
            states.Add( HumanState.TALK,new Talk());
            states.Add(HumanState.LEARN,new Learn());
            states.Add(HumanState.PAUSE,new Pause());
            states.Add(HumanState.WALK,new Walk());
            states.Add(HumanState.COMMUNICATION,new Communication());
            states.Add(HumanState.QUITED,new Quited());
            
            EmployeeData.Company
                .GetOffice(EmployeeData.GetHisOffice)
                .applyWorkerEvent(this);
        }
        public EmployeeData EmployeeData { get => employeeData; set => employeeData = value; }

        public void Work()
        {
            gameObject.name = EmployeeData.GetEntityType.ToString();
            navMeshAgent = GetComponent<NavMeshAgent>();
            EmployeeData.Home = transform.position;
            RegisterAn();
            
            //employeeStates = EmployeeData.EntityWorkCycle.Keys.ToList();
            //StartCoroutine(LifeCycle());
            //StartCoroutine(ShowMyCanvas());
            StateController();
        }

        #region oldversion
        protected float ActivityDuration()
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
        protected Activity GetActivity()
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
        
        protected override IEnumerator LifeCycle()
        {
            int index = 0;
            Vector3 targetPosition = Vector3.zero;
            GoToOffice(out targetPosition);
            
            while (SelfState.CurrentState != HumanState.QUITED)
            {
                if (IsOnPosition(targetPosition))
                {
                    activityDuration = ActivityDuration();
                    // when the activity is a task for the Project,
                    // than need to work on them
                    if(SelfState.CurrentState == HumanState.WORK)
                    {
                        #region Work on the Task 
                        
                        chore = GetActivity(); // Task/Activity for the Employee
                        
                        //Debug.Log($"State : {SelfState.CurrentState} , chore {chore}");
                        if(chore != null)
                        {
                            task = WaitOfDoneTask(chore, activityDuration); // Threading.Task
                            awaiter = task.GetAwaiter(); // TaskAwaiter
                            do
                            {
                                //Do something
                                //Debug.Log($"State : {SelfState.CurrentState} , chore {chore}");
                                yield return null;
                            } while (!awaiter.IsCompleted);
                        }

                        #endregion
                    }
                    else
                    {
                       // Debug.Log($"{SelfState.CurrentState} , activityDuration : {activityDuration}");
                        // Make a Pause or talk with the others
                        yield return new WaitForSeconds(activityDuration);
                    }
                    if(SelfState.CurrentState != HumanState.QUITED)
                    {
                        index = index >= employeeStates.Count ? 0 : index;
                        // after he finished,
                        // it going to the next Office and make yours Activity
                        //Debug.Log($"{SelfState.CurrentState}");
                        destination = EmployeeData.EntityWorkCycle[employeeStates[index]];
            
                        //Debug.Log($"index {index} , Destination {destination}");
            
                        targetPosition = EmployeeData.OfficePosition(destination);
                        
                        SelfState.CurrentState = employeeStates[index];
                        
                        targetPosition = GenerateRandomPosition(targetPosition);

                        Navigator.MoveTo(navMeshAgent, targetPosition);
                        index++;
                    }
                    
                }
                
                // hier passiert alles wärend des Laufens
                if (Navigator.MyPathStatus(navMeshAgent) == PathProgress.NONE)
                {
                    
                    Debug.Log($"{SelfState.CurrentState}");
                    targetPosition = GenerateRandomPosition(EmployeeData.MyOfficePosition);
                }
                
                yield return null;
            }
            GoHome(EmployeeData.Home).Start();
        }

        private async Task GoHome(Vector3 targetPosition)
        {
            destination = BuildingType.NONE;
            Navigator.MoveTo(navMeshAgent,targetPosition);

            while (SelfState.CurrentState == HumanState.QUITED)
            {
                if (IsOnPosition(targetPosition))
                {
                    Destroy(gameObject);
                    await Task.Delay(1);
                    return;
                }
            }
        }
        private void GoToOffice(out Vector3 targetPosition)
        {
            //That need to be combined
            destination = EmployeeData.GetHisOffice;
            targetPosition = GenerateRandomPosition(EmployeeData.MyOfficePosition);
            //-------------------------------
            
            Navigator.MoveTo(navMeshAgent,targetPosition);
        }
        private IEnumerator ShowMyCanvas()
        {
            namePoster = proceduralUiElements.GetCanvas(this.EmployeeData.GetEntityType.ToString());
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

        private async Task WaitOfDoneTask(Activity activity,float taskDuration)
        {
            activity.ApplyTaskTaker(this);
                        
            taskDuration = activity.TimeDuration;
            var taskProgressBar = activity.ProgressBar;
                            
            var time = 0f;
            var smoothDeltaTime = Time.smoothDeltaTime;
            var part = taskProgressBar.howMuchNeed / (taskDuration / smoothDeltaTime);
                            
            while (time <= taskDuration && SelfState.CurrentState != HumanState.QUITED)
            {
                time += smoothDeltaTime;
                                
                taskProgressBar.percentDoneProgress += part;
                activity.ProgressBar = taskProgressBar;

                await Task.Delay((int) smoothDeltaTime);
            }
        }
        private bool IsOnPosition(Vector3 targetPosition)
        {
            return (Math.Abs(transform.position.x - targetPosition.x) <= 0.1f && Math.Abs(transform.position.z - targetPosition.z) <= 0.1f);
        }
        #endregion

        private void StateController()
        {
            var state = states[HumanState.WORK];
            state.emploee = this;
            stateMachineClass.onStateEnter += OnEnter;
            stateMachineClass.onStateUpdated += OnUpdate;
            stateMachineClass.onStateExit += OnExit;
            stateMachineClass.onStateCompleted += GoNext;
            stateMachineClass.CurrentState = state;
        }

        private void OnEnter()
        {
            stateMachineClass.CurrentState.OnStateEnter();
        }
        private IEnumerator OnUpdate()
        {
            while (true)
            {
                stateMachineClass.CurrentState.OnStateUpdate();
                yield return null;
            }
        }
        private void OnExit()
        {
            stateMachineClass.CurrentState.OnStateExit();
        }
        private void GoNext()
        {
            Debug.Log($"current State {stateMachineClass.CurrentState} is Completed");
        }
        
        private void OnDestroy()
        {
            Destroy(namePoster);
        }
    }
}