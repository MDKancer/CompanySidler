using System.Collections.Generic;
using BootManager;
using BuildingPackage;
using Enums;
using GameCloud;
using ProjectPackage;
using UnityEngine;
using Zenject;

namespace Human
{
    public class EmployeeData
    {
        /// <summary>
        /// Wie viel kriegt ein Mitarbeiter pro "Stunde".
        /// </summary>
        public int hourlyWage = 10;
        
        private EntityType entityType = EntityType.NONE;
        private BuildingType hisOffice = BuildingType.NONE;
        private Project project;
        private Dictionary<HumanState,BuildingType> entityWorkCycle = new Dictionary<HumanState, BuildingType>();
        private GameObject prefab = null;

        private Company company;

        public EmployeeData(Company company,GameObject prefab,EntityType entityType, BuildingType hisOffice)
        {
            this.company = company;
            this.hisOffice = hisOffice;
            this.entityType = entityType;
            this.prefab = prefab;
            SetDataFields();
        }
        public GameObject GetPrefab => prefab;

        public Company Company => company;

        public Project Project
        {
            get => project;
            set => project = value;
        }

        public BuildingType GetHisOffice { 
            get => hisOffice;
            set => hisOffice = value;
        }
        public Dictionary<HumanState,BuildingType> GetEntityWorkingCycle { get=> entityWorkCycle; }
        public EntityType GetEntityType { get=> entityType; }

        private void SetDataFields()
        {
         SetEntityWorkCycleField();
        }


        private void SetEntityWorkCycleField()
        {
            if (entityType != EntityType.NONE && entityType != EntityType.CUSTOMER)
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