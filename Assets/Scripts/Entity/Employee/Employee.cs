using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private int index = -1;
        //_____
         
        public StateMachineClass<EmployeeState> stateMachineClass;

        private Dictionary<HumanState,EmployeeState> states;
        private void RegisterAn()
        {
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

        public void Work(DiContainer diContainer)
        {
            gameObject.name = EmployeeData.GetEntityType.ToString();
            navMeshAgent = GetComponent<NavMeshAgent>();
            EmployeeData.Home = transform.position;
            stateMachineClass = diContainer.Resolve<StateMachineClass<EmployeeState>>();
            RegisterAn();
            AttachEvent();
            GoNext();
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
            // Debug.Log($"current State {stateMachineClass.CurrentState} is Completed");
            SetState();
        }

        private void SetState()
        {
            // if(stateMachineClass.LastState != null) DetachEvent();
            index = index < states.Count-2 ? index+1 : 0;
            var state = states.ElementAt(index).Value; // State
            Debug.Log(state);
            state.emploee = this;
            
            stateMachineClass.CurrentState = state;
        }
        
        private void AttachEvent()
        {
            stateMachineClass.onStateEnter += OnEnter;
            stateMachineClass.onStateUpdated += OnUpdate;
            stateMachineClass.onStateExit += OnExit;
            stateMachineClass.onStateCompleted += GoNext;
        }
        private void DetachEvent()
        {
            stateMachineClass.onStateEnter -= OnEnter;
            stateMachineClass.onStateUpdated -= OnUpdate;
            stateMachineClass.onStateExit -= OnExit;
            stateMachineClass.onStateCompleted -= GoNext;
        }
        
        private void OnDestroy()
        {
            DetachEvent();
            Destroy(namePoster);
        }
    }
}