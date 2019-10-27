using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Human;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class MarketingTown : Building, iMarketing
    {

        void Awake()
        {
            this.budget = 0;
            buildingData = new BuildingData
            {
                buildingType = BuildingType.MARKETING,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -3,
                
                AccessibleWorker = new List<BuildingWorkers<Worker, EntityType>>
                {
                    new BuildingWorkers<Worker, EntityType>(EntityType.DESIGNER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.PRODUCT_OWNER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Worker, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TEAM_LEADER),
                    new BuildingWorkers<Worker, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Worker, EntityType>(EntityType.DESIGNER),
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
        //coroutine -> alternative zu update

        public int ToTrade()
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
                    budget += ToTrade();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}