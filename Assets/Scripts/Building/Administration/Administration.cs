using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Human;
using StateMachine;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class Administration : Building, iAdministration
    {
        void Awake()
        {
            this.budget = 0;
            buildingData = new BuildingData
            {
                buildingType = BuildingType.ADMIN,
                name = name,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 5,
                
                
                AccessibleWorker = new List<BuildingWorkers<Worker, EntityType>>
                {
                    new BuildingWorkers<Worker, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Worker, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Worker, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TEAM_LEADER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TESTER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern / Ändern
                }
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }

        public int Supporting()
        {
            return BuildingData.wastage; 
        }

        public void SwitchWorkingState()
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
                    budget += Supporting();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}