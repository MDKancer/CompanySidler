using System.Collections.Generic;
using BuildingPackage.OfficeWorker;
using Constants;
using Life;

namespace BuildingPackage
{
    public struct BuildingData
    {
        public string name;
        public BuildingType buildingType;
        public float upgradePrice;
        public int workPlacesLimit;
        public int workers;
        public int moneyPerSec;
        public int maxHitPoints;
        public int currentHitPoints;
        private List<BuildingWorker<Human,EntityType>> accessibleWorker;
        /// <summary>
        /// Get the actual number of employed Workers .....
        /// </summary>
        /// <param name="workerType"></param>
        /// <returns> int[0] -> is the number of the Workers from Type <param name="workerType"></param>
        /// int[1] -> is the maximal places for this workers.</returns>
        public (int employedPlaces, int countEmployedPlaces) GetCountOfEmployedWorkers(EntityType workerType)
        {
            var countEmployedPlaces = 0;
            var employedPlaces = 0;
            for (int i = 0; i < AccessibleWorker.Count; i++)
            {
                if (AccessibleWorker[i].WorkerType == workerType)
                {
                    countEmployedPlaces++;
                    if (AccessibleWorker[i].Worker != null)
                    {
                        employedPlaces++;
                    }
                }
            }

            return (employedPlaces, countEmployedPlaces);
        }

        public List<BuildingWorker<Human, EntityType>> AccessibleWorker
        {
            get
            {
                var temp = new List<BuildingWorker<Human, EntityType>>();

                for (int i = 0; i < workPlacesLimit; i++)
                {
                    temp.Add(accessibleWorker[i]);
                }

                return temp;
            }
            set => accessibleWorker = value;
        }
        
    }
}