using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Life;
using UIPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class Office : Building, iOffice
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.OFFICE,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -2,
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
        public int Managment()
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
                    money += Managment();
                    UIDispatcher.currentBudget += Managment();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}