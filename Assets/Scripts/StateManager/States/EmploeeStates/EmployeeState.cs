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
using VideoManager;
using Zenject;

namespace StateManager.States.EmploeeStates
{
    public class EmployeeState : AState
    {
        public Employee emploee;
                
        protected Activity chore;
        protected float taskDuration;
        protected float time = 0;
        protected float part;
        protected (float percentDoneProgress, float howMuchNeed) taskProgressBar;
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

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
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
    }
}