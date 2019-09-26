using System.Collections.Generic;
using BuildingPackage.Worker;
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
        public int currenHhitPoints;
        public List<BuildingWorker<Human,EntityType>> accesibleWorker;


    }
}