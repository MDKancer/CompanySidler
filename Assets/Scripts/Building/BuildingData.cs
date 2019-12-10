using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Enums;
using Human;
using UnityEngine;

namespace BuildingPackage
{
    public struct BuildingData
    {
        public string name;
        public BuildingType buildingType;
        public GameObject prefab;
        /// <summary>
        /// der Preis des Gebäude zum kaufen.
        /// </summary>
        public int price;
        public float upgradePrice;
        public int workPlacesLimit;
        public int workers;
        public int moneyPerSec;
        public int maxHitPoints;
        public int currentHitPoints;
        
        /// <summary>
        /// wie viel braucht ein Office derren Mitarbeitern zu bezahlen.
        /// </summary>
        public int wastage;
        
        private List<BuildingWorkers<Employee,EntityType>> availableWorker;
        
        /// <summary>
        /// Get the actual number of employed Workers .....
        /// </summary>
        /// <param name="workerType"></param>
        /// <returns> int employedPlaces -> is the number of the Workers from Type <param name="workerType"></param>
        /// int countEmployedPlaces -> is the maximal places for this workers.</returns>
        public (int employedPlaces, int countEmployedPlaces) GetCountOfEmployed(EntityType workerType)
        {
            var countEmployedPlaces = 0;
            var employedPlaces = 0;
            for (int i = 0; i < AvailableWorker.Count; i++)
            {
                if (AvailableWorker[i].WorkerType == workerType)
                {
                    countEmployedPlaces++;
                    if (AvailableWorker[i].Worker != null)
                    {
                        employedPlaces++;
                    }
                }
            }

            return (employedPlaces, countEmployedPlaces);
        }

        public List<BuildingWorkers<Employee, EntityType>> AvailableWorker
        {
            get => availableWorker.GetRange(0,workPlacesLimit);
            set => availableWorker = value;
        }

        /// <summary>
        /// der Verbraucht des Gebäude steigert/ niedrigt abhängig von Anzahl des Mitarbeitern.
        /// </summary>
        public void ChangeWastage()
        {
            if(AvailableWorker.Count > 0)
            {
                foreach (var worker in AvailableWorker)
                {
                    if(worker.Worker != null)
                    {
                        wastage -= worker.Worker.EmployeeData.hourlyWage;
                    }
                }
            }
            else
            {
                wastage = 0;
            }
        }
        
    }
}