using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
using Enums;
using Human;
using NaughtyAttributes;
using ProjectPackage;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;

namespace BuildingPackage
{
    public class Building : MonoBehaviour,iBuilding
    {
        public int budget;
        public List<Project> possibleProjects = new List<Project>(3);
        [SerializeField,Required]
        protected  GameObject officePrefab;
        
        protected float buildTime;
        protected Company company;
        protected StateController<BuildingState> stateController = new StateController<BuildingState>();
        protected BuildingData buildingData;
        
        [ShowNonSerializedField]
        protected bool isBuying = false;
        
        public delegate void ApplyWorkerEvent(Employee employee);

        public ApplyWorkerEvent applyWorkerEvent;
        private Project project = null;
        [Inject]
        private SpawnController spawnController;
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
                    
                    if(project != null && VARIABLE.Worker.EmployeeData.Project == null)
                    {
                        VARIABLE.Worker.EmployeeData.Project = project;
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
            if(buildingData.workers > 0 && project == null)
            {
                this.project = newProject;
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
        public Project Project => project;

        public virtual bool IsBuying { set;get;}

        public BuildingData BuildingData => buildingData;
        public BuildingState buildingWorkingState => stateController.CurrentState;

        public Company Company
        {
            get => company;
            set => company = value;
        }

        private IEnumerator CheckIfProjectIsDone()
        {
            while (!project.IsDone) yield return null;
            var particleSystem =  spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
            Destroy(particleSystem.gameObject, 2f);
            project = null;
        }
    }
}