using System.Collections.Generic;
using BootManager;
using Constants;
using ProjectPackage;
using UnityEngine;

namespace Human
{
    public class WorkerData
    {
        private GameObject prefab = null;
        public int hourlyWage = 10;
        private EntityType entityType = EntityType.NONE;
        private BuildingType hisOffice = BuildingType.NONE;
        private Project project;
        private Dictionary<HumanState,BuildingType> entityWorkCycle = new Dictionary<HumanState, BuildingType>();

        public WorkerData(EntityType entityType, BuildingType hisOffice)
        {
            this.hisOffice = hisOffice;
            this.entityType = entityType;
            SetDataFields();
        }
        public GameObject GetPrefab { get => prefab; }

        public Project Project
        {
            get => project;
            set
            {
                project = value;
            }
        }

        public BuildingType GetHisOffice { 
            get => hisOffice;
            set => hisOffice = value;
        }
        public Dictionary<HumanState,BuildingType> GetEntityWorkingCycle { get=> entityWorkCycle; }
        public EntityType GetEntityType { get=> entityType; }

        private void SetDataFields()
        {
         SetPrefabField();
         SetEntityWorkCycleField();
        }

        private void SetPrefabField()
        {
            if (entityType != EntityType.NONE && entityType != EntityType.CLIENT)
            {
               prefab = Boot.container.GetPrefabsByType(EntityType.DEVELOPER)[0];
            }
        }

        private void SetEntityWorkCycleField()
        {
            if (entityType != EntityType.NONE && entityType != EntityType.CLIENT)
            {
                    entityWorkCycle.Add(HumanState.WORK,hisOffice);
                    entityWorkCycle.Add(HumanState.TALK,BuildingType.SOCIAL_RAUM);
                    entityWorkCycle.Add(HumanState.PAUSE,BuildingType.SOCIAL_RAUM);
                    entityWorkCycle.Add(HumanState.LEARN,BuildingType.AZUBIS);
                    entityWorkCycle.Add(HumanState.WALK,BuildingType.NONE);
            }
        }
    }
}