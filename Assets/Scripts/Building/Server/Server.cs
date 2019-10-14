using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Life;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class Server : Building, iServer
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.SERVER,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 5,
                
                AccessibleWorker = new List<BuildingWorker<Human, EntityType>>
                {
                    new BuildingWorker<Human, EntityType>(EntityType.TEAMLEADER),
                    new BuildingWorker<Human, EntityType>(EntityType.TESTER),
                    new BuildingWorker<Human, EntityType>(EntityType.ANALYST),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.DESIGNER),
                    new BuildingWorker<Human, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorker<Human, EntityType>(EntityType.AZUBI)
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

        public int Hosten()
        {
            return buildingData.workers * buildingData.moneyPerSec;
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
                    money += Hosten();
                    UIDispatcher.currentBudget += Hosten();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}