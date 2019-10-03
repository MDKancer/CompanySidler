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
        public int currenHhitPoints;
        public List<BuildingWorker<Human,EntityType>> accesibleWorker;


    }
}