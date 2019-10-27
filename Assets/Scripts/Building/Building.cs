using System.Collections;
using System.Collections.Generic;
using Constants;
using Human;
using ProjectPackage;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Building : MonoBehaviour,iBuilding
    {
        public int budget;
        private Project project = null;
        protected float buildTime;

        protected Company company;

        public List<Project> possibleProjects = new List<Project>(3);
//        protected BuildingState buildingState;
        protected StateController<BuildingState> stateController = new StateController<BuildingState>();
        protected BuildingData buildingData;


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

        public void Work()
        {
        }

        public void SwitchWorkingState()
        {
          
        }

        public void ApplyWorker(Worker worker)
        {
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
            {
                if (VARIABLE.WorkerType == worker.WorkerData.GetEntityType && VARIABLE.Worker == null)
                {
                    VARIABLE.Worker = worker;
                    buildingData.workers++;
                    buildingData.ChangeWastage();
                    if(project != null && VARIABLE.Worker.WorkerData.Project == null)
                    {
                        VARIABLE.Worker.WorkerData.Project = project;
                    }
                    return;
                }
            }
        }

        public void QuitWorker(Worker worker)
        {
            foreach (var VARIABLE in BuildingData.AccessibleWorker)
            {
                if (VARIABLE.WorkerType == EntityType.TEAM_LEADER && VARIABLE.Worker != null)
                {
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
                    if( worker.Worker!= null && worker.Worker.WorkerData.Project == null)
                    {
                        worker.Worker.WorkerData.Project = newProject;
                    }
                }

                StartCoroutine(CheckIfProjectIsDone());
            }
        }

        public bool BuildingRepair()
        {
            return false;
        }

        

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
            project = null;
        }
    }
}