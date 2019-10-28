using System.Collections;
using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Enums;
using Human;
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
                
                AccessibleWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.TESTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVOPS),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ADMIN),
                    new BuildingWorkers<Employee, EntityType>(EntityType.AZUBI)
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
                    budget += Hosten();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}