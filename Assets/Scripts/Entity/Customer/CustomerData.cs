using System;
using BootManager;
using BuildingPackage;
using Enums;
using GameCloud;
using ProjectPackage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Customer.Data
{
    public class CustomerData
    {
        public readonly GameObject prefab;
        public readonly Company company;
        public Project project;
        public readonly CustomerType customerType;
        public readonly Vector3 initialPosition;
        private Vector3 targetPosition;
        
        private readonly Container container;
        private readonly TarentTown tarentTown;
        private CustomerLevelData customerLevelData;
        
        public CustomerData(CustomerType customerType,Vector3 spawnPosition)
        {
            customerLevelData = new CustomerLevelData();
            
            this.initialPosition = spawnPosition;
            this.customerType = customerType;
            
            this.container = Boot.container;
            this.company = container.Companies[0];
            tarentTown = (TarentTown) company.GetOffice(BuildingType.TARENT_TOWN);
            this.prefab = container.GetPrefabsByType(EntityType.CUSTOMER)[0];
            
        }

        public void GenerateNewProject()
        {
            var maxCountTasks = Enum.GetValues(typeof(TaskType)).Length;
            var randomCountTasks = Random.Range(5, maxCountTasks);
            this.project = new Project(randomCountTasks);
            project.customerType = customerType;
        }

        public Vector3 TarentTownPosition ()
        {
            targetPosition = tarentTown.gameObject.transform.position;

            return targetPosition;
        }

        public void SellProject()
        {
            tarentTown.TakeProject(ref project,customerType);
        }
    }
}