using System.Collections;
using System.Collections.Generic;
using Entity.Employee;
using Enums;
using UnityEngine;

namespace Buildings.Accounting
{
    public class Accounting : Building, iAccounting
    {
        void Awake()
        {
            this.budget = 0;
            buildingData = new BuildingData
            {
                buildingType = BuildingType.ACCOUNTING,
                name = name,
                prefab =  this.officePrefab,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                price = 0,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -3,
                
                AvailableWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.ACCOUNTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ACCOUNTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.TEAM_LEADER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ACCOUNTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ACCOUNTER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DEVELOPER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.AZUBI)
                    //TODO : Die Liste Erweitern / Ändern
                }
                
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }
        public int Compute()
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
        
        public override bool IsBuying
        {
            get => isBuying;
            set
            {
                Buy(buildingData.prefab, this.transform.position);
                isBuying = value;
            }
        }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    if (company != null)
                    {
                        company.CurrentBudget += Compute();
                        budget += Compute();
                    }
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}