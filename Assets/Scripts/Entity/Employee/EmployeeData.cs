using System;
using System.Collections.Generic;
using Buildings;
using Enums;
using ProjectPackage;
using UnityEngine;

namespace Entity.Employee
{
    public class EmployeeData
    {
        /// <summary>
        /// Wie viel kriegt ein Mitarbeiter pro "Stunde".
        /// </summary>
        public int hourlyWage = 10;
        public readonly Dictionary<HumanState,BuildingType> EntityWorkCycle = new Dictionary<HumanState, BuildingType>();
        public EmployeeData(Company company,GameObject prefab,EntityType entityType, BuildingType hisOffice)
        {
            this.Company = company;
            this.GetHisOffice = hisOffice;
            this.GetEntityType = entityType;
            this.GetPrefab = prefab;
            SetDataFields();
        }
        public GameObject GetPrefab { get; }
        public Company Company { get; }
        public Project Project { get; set; }
        public BuildingType GetHisOffice { get;}
        public EntityType GetEntityType { get; }
        public Vector3 Home { get; set; }

        public Vector3 MyOfficePosition => Company.GetOffice(GetHisOffice).transform.position;

        public Vector3 OfficePosition(BuildingType buildingType)
        {
            return Company.GetOffice(buildingType).transform.position;
        }
        private void SetDataFields()
        {
            SetEntityWorkCycleField();
        }
        private void SetEntityWorkCycleField()
        {
            if (GetEntityType != EntityType.NONE && GetEntityType != EntityType.CUSTOMER)
            {
                    EntityWorkCycle.Add(HumanState.WORK,GetHisOffice);
                    EntityWorkCycle.Add(HumanState.TALK,BuildingType.MARKETING);
                    EntityWorkCycle.Add(HumanState.PAUSE,BuildingType.SOCIAL_RAUM);
                    EntityWorkCycle.Add(HumanState.COMMUNICATION,BuildingType.OFFICE);
                    EntityWorkCycle.Add(HumanState.LEARN,BuildingType.AZUBIS);
                    EntityWorkCycle.Add(HumanState.WALK,BuildingType.SOCIAL_RAUM);
            }
        }
    }
}