using System;
using AudioManager;
using Container;
using Entity.Employee;
using Enums;
using InputManager;
using JetBrains.Annotations;
using ProjectPackage.ProjectTasks;
using So_Template;
using SpawnManager;
using StateManager.States.GameStates.Template;
using UnityEngine;
using UnityEngine.AI;
using VideoManager;
using Zenject;
using Random = UnityEngine.Random;

namespace StateManager.States.EmploeeStates
{
    public class EmployeeState : AState
    {
        public Employee emploee;
        public BuildingType destination = BuildingType.NONE;
        protected NavMeshAgent navMeshAgent;
        protected Vector3 targetPosition;
        protected Activity chore;
        protected float duration;
        protected float time = 0;
        protected float part;
        protected (float percentDoneProgress, float howMuchNeed) progress;
        public override void Init(SignalBus signalBus, Cloud cloud, StateController<RunTimeState> runTimeStateController, InputController inputController,
            AudioController audioController, VideoController videoController, MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager, SpawnController spawnController, CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.runTimeStateController = runTimeStateController;
            this.inputController = inputController;
            this.audioController = audioController;
            this.videoController = videoController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            this.companyData = companyData;
        }

        public override void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
        protected Vector3 GenerateRandomPosition(Vector3 position)
        {
            return new Vector3(
                Random.Range(position.x-5, position.x+5),      // x
                position.y,                                    // y
                Random.Range(position.z-5, position.z+5)    // z
            );
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
        protected EmployeeData EmployeeData => emploee.EmployeeData;
        
        protected bool IsOnPosition(Vector3 targetPosition)
        {
            return (Mathf.Abs(emploee.transform.position.x - targetPosition.x) <= 0.1f && Math.Abs(emploee.transform.position.z - targetPosition.z) <= 0.1f);
        }

        protected float GetActivityDuration
        {
            get
            {
               var value = Random.Range(1f, 2f);
               // Debug.Log(value);
               return value;
            }
        }
            
    }
}