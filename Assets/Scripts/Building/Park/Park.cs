using System.Collections;
using Constants;
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

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }
        public int Relax()
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
                    budget += Relax();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}