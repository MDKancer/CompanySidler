using System.Collections;
using System.Collections.Generic;
using BootManager;
using Enums;
using Human;
using ProjectPackage;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Building : MonoBehaviour,iBuilding
    {
        public int budget;
        public List<Project> possibleProjects = new List<Project>(3);
        public bool isBuying = false;
        
        protected float buildTime;
        protected Company company;
        protected StateController<BuildingState> stateController = new StateController<BuildingState>();
        protected BuildingData buildingData;

        private Project project = null;

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


        public void SwitchWorkingState()
        {
          
        }

        public void ApplyWorker(Employee employee)
        {
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
            {
                if (VARIABLE.WorkerType == employee.EmployeeData.GetEntityType && VARIABLE.Worker == null)
                {
                    VARIABLE.Worker = employee;
                    buildingData.workers++;
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
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
            {
                if (VARIABLE.WorkerType == employee.EmployeeData.GetEntityType && VARIABLE.Worker != null)
                {
                    VARIABLE.Worker.SelfState.CurrentState = HumanState.QUITED;
                    VARIABLE.Worker = null;
                    buildingData.workers--;
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
                
                foreach (var worker in BuildingData.AccessibleWorker)
                {
                    if( worker.Worker!= null && worker.Worker.EmployeeData.Project == null)
                    {
                        worker.Worker.EmployeeData.Project = newProject;
                    }
                }
                StartCoroutine(CheckIfProjectIsDone());
                var particleSystem = Boot.spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
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
            var particleSystem =  Boot.spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
            Destroy(particleSystem.gameObject, 2f);
            project = null;
        }
    }
}