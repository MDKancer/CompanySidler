using System.Collections.Generic;
using BootManager;
using Constants;
using UnityEngine;

namespace Life
{
    public class HumanData
    {
        private GameObject prefab = null;
        private EntityType entityType = EntityType.NONE;
        private BuildingType hisOffice = BuildingType.NONE;
        private Dictionary<HumanState,BuildingType> entityWorkigCycle = new Dictionary<HumanState, BuildingType>();

        public HumanData(EntityType entityType)
        {
            this.entityType = entityType;
            SetDataFields();
        }
        public GameObject GetPrefab { get => prefab; }
        public BuildingType GetHisOffice { 
            get => hisOffice;
            set => hisOffice = value;
        }
        public Dictionary<HumanState,BuildingType> GetEntityWorkingCycle { get=> entityWorkigCycle; }
        public EntityType GetEntityType { get=> entityType; }

        private void SetDataFields()
        {
         SetPrefabField();
         SetEntityWorkigCycleField();
        }

        private void SetPrefabField()
        {
            if (entityType != EntityType.NONE && entityType != EntityType.CLIENT)
            {
               prefab = Boot.container.GetPrefabsByType(EntityType.DEVELOPER)[0];
            }
        }

        private void SetEntityWorkigCycleField()
        {
            if (entityType != EntityType.NONE && entityType != EntityType.CLIENT)
            {
                    entityWorkigCycle.Add(HumanState.WORK,hisOffice);
                    entityWorkigCycle.Add(HumanState.PAUSE,BuildingType.SOCIAL_RAUM);
                    entityWorkigCycle.Add(HumanState.LEARN,BuildingType.AZUBIS);
                    entityWorkigCycle.Add(HumanState.WALK,BuildingType.NONE);
            }
        }
    }
}