using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Human;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class AzubisTown : Building, iAzubis
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.AZUBIS,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -4,
                
                AccessibleWorker = new List<BuildingWorkers<Worker, EntityType>>
                {
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.AZUBI),
                    new BuildingWorkers<Worker, EntityType>(EntityType.TEAM_LEADER),
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

        public int ToLearn()
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
                    budget += ToLearn();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}