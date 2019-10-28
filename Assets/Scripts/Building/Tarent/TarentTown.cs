using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
using BuildingPackage.OfficeWorker;
using Enums;
using JetBrains.Annotations;
using Human;
using ProjectPackage;
using StateMachine;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class TarentTown : Building, iTarent
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.TARENT_TOWN,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -8,
                
                AccessibleWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DESIGNER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL)
                    //TODO : Die Liste Erweitern / Ändern
                }
            };
            
            stateController.CurrentState = BuildingState.EMPTY;
        }
        
        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
            StartCoroutine(DistributeProjects());
        }

        public void TakeProject(Project newProject,ClientType clientType)
        {
            company.AddNewProject(newProject, clientType);
        }
        public int ToHold()
        {
            return BuildingData.wastage; 
        }
        public new void SwitchWorkingState()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                stateController.CurrentState = BuildingState.PAUSE;
                StopCoroutine(UpdateManyGenerator());
            }
            else
            {
                stateController.CurrentState = BuildingState.WORK;
                StartCoroutine( UpdateManyGenerator());
            }
        }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    budget += ToHold();
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private IEnumerator DistributeProjects()
        {
            var clientValues = Enum.GetValues(typeof(ClientType));
            var buidingValues = Enum.GetValues(typeof(BuildingType));
            
            var buildings = Boot.container.Firmas;
            
            while (true)
            {
                foreach (var clientValue in clientValues)
                {
                    foreach (var buidingValue in buidingValues)
                    {
                        if (clientValue.ToString().Contains(buidingValue.ToString()))
                        {
                            Building building = buildings[0].GetOffice((BuildingType) buidingValue);
                            building.possibleProjects = company.GetProjectsByType((ClientType) clientValue);
                        }
                    }
                }
                
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}