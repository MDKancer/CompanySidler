using System.Collections;
using BootManager;
using Enums;
using UnityEngine;

namespace BuildingPackage
{
    public class Park : Building, iPark
    {
        void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.NONE,
                prefab =  this.officePrefab,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -5,
                
                // Keine Liste mit Mitarbeiter
                
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }
        public int Relax()
        {
            return BuildingData.wastage; 
        }
        public override void SwitchWorkingState()
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
                    budget += Relax();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}