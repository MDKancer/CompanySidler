using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Human;
using NaughtyAttributes;
using ProjectPackage;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace BuildingPackage
{
    public class Building : MonoBehaviour,iBuilding
    {
        public delegate void ApplyWorkerEvent(Employee employee);
        public ApplyWorkerEvent applyWorkerEvent;
        
        public List<Project> possibleProjects = new List<Project>(3);
        
        public int budget;
        [SerializeField,Required] 
        protected  GameObject officePrefab;
        [ShowNonSerializedField]  
        protected bool isBuying = false;
        protected float buildTime;
        protected Company company;
        protected StateController<BuildingState> stateController = new StateController<BuildingState>();
        protected BuildingData buildingData;

        protected SpawnController spawnController;
        protected SignalBus signalBus;
        

        private Project currentProject = null;
        
        [Inject]
        protected virtual void Init(SignalBus signalBus, SpawnController spawnController)
        {
            this.signalBus = signalBus;
            this.spawnController = spawnController;
            this.signalBus.Subscribe<GameStateSignal>(StateDependency);
        }
        public void OnEnable()
        {
            applyWorkerEvent += ApplyWorker;
        }
        public void Upgrade()
        {
            buildingData.workPlacesLimit += 5;
            buildingData.upgradePrice *= 2;
            buildingData.moneyPerSec *= 2;
        }
        public void DoDamage(int damagePercent = 0)
        {
            buildingData.currentHitPoints -= damagePercent;
        }

        /// <summary>
        /// den Zustand des Gebüude ändernt.
        /// </summary>
        public virtual void SwitchWorkingState()
        {
          
        }
        public void ApplyWorker(Employee employee)
        {
            foreach (var VARIABLE in BuildingData.AvailableWorker)
            {
                if (VARIABLE.WorkerType == employee.EmployeeData.GetEntityType && VARIABLE.Worker == null)
                {
                    VARIABLE.Worker = employee;
                    buildingData.workers++;
                    
                    //Abhängig von Anzahl des Mitarbeiter wird der Verbrauch reduziert.
                    buildingData.ChangeWastage();
                    
                    if(currentProject != null && VARIABLE.Worker.EmployeeData.Project == null)
                    {
                        VARIABLE.Worker.EmployeeData.Project = currentProject;
                    }
                    return;
                }
            }
        }

        public void QuitWorker(Employee employee)
        {
            foreach (var VARIABLE in BuildingData.AvailableWorker)
            {
                if (VARIABLE.WorkerType == employee.EmployeeData.GetEntityType && VARIABLE.Worker != null)
                {
                    VARIABLE.Worker.SelfState.CurrentState = HumanState.QUITED;
                    VARIABLE.Worker = null;
                    buildingData.workers--;
                    //Abhängig von Anzahl des Mitarbeiter wird der Verbrauch befördert.
                    buildingData.ChangeWastage();
                    return;
                }
            }
        }

        public void ApplyProject(Project newProject)
        {
            if(buildingData.workers > 0 && currentProject == null)
            {
                this.currentProject = newProject;
                budget = newProject.Budget;
                
                foreach (var worker in BuildingData.AvailableWorker)
                {
                    if( worker.Worker!= null && worker.Worker.EmployeeData.Project == null)
                    {
                        worker.Worker.EmployeeData.Project = newProject;
                    }
                }
                StartCoroutine(CheckIfProjectIsDone());
                var particleSystem = spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
                Destroy(particleSystem.gameObject, 2f);
            }
        }

        public bool BuildingRepair()
        {
            return false;
        }

        /// <summary>
        /// Das Project wird automatisch gelöscht/zerstört, wenn das ganzes Projekt "Done" ist.
        /// </summary>
        public Project CurrentProject => currentProject;

        public virtual bool IsBuying { set;get;}

        public BuildingData BuildingData => buildingData;
        public BuildingState buildingWorkingState => stateController.CurrentState;

        public Company Company
        {
            get => company;
            set => company = value;
        }

        protected virtual void StateDependency(GameStateSignal gameStateSignal)
        {
            switch (gameStateSignal.state)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    stateController.CurrentState = BuildingState.WORK;
                    Debug.Log("UpdateManyGenerator");
                    StartCoroutine(UpdateManyGenerator());
                    break;
                case GameState.EXIT:
                    break;
            }
        }

        protected virtual IEnumerator UpdateManyGenerator(){yield return null;}
        

        /// <summary>
        /// Beim einkaufs des Büro werden die Objecte gespawnt. 
        /// </summary>
        protected void Buy(GameObject prefab, Vector3 position)
        {
            if (!isBuying)
            {
                spawnController.SpawnOffice(prefab, position);
            }
        }
        protected virtual void OnApplicationQuit()
        {
            Debug.Log("Unsubscribe");
            signalBus.TryUnsubscribe<GameStateSignal>(StateDependency);
        }
        protected virtual void OnDestroy()
        {
            signalBus.TryUnsubscribe<GameStateSignal>(StateDependency);
        }

        protected void OnDisable()
        {
            signalBus.TryUnsubscribe<GameStateSignal>(StateDependency);
        }

        private IEnumerator CheckIfProjectIsDone()
        {
            while (!currentProject.IsDone) yield return null;
            var particleSystem =  spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
            Destroy(particleSystem.gameObject, 2f);
            currentProject = null;
        }
    }
}