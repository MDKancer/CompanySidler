using System;
using Buildings;
using Buildings.Tarent;
using Enums;
using ProjectPackage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Customer
{
    public class CustomerData
    {
        public readonly GameObject prefab;
        public readonly Company company;
        public Project project;
        public readonly CustomerType customerType;
        public readonly Vector3 initialPosition;
        private Vector3 targetPosition;

        private readonly TarentTown tarentTown;
        private CustomerLevelData customerLevelData;
        private MonoBehaviour monoBehaviour;

        //TODO: es muss ohne Container Fuktionieren!!!
        public CustomerData(CustomerType customerType,Vector3 spawnPosition,Container.Cloud cloud,MonoBehaviour monoBehaviour)
        {
            customerLevelData = new CustomerLevelData();
            
            this.initialPosition = spawnPosition;
            this.customerType = customerType;

            this.company = cloud.Companies[0];
            tarentTown = (TarentTown) company.GetOffice(BuildingType.TARENT_TOWN);
            
            this.prefab = cloud.GetPrefabsByType(EntityType.CUSTOMER)[0];
            this.monoBehaviour = monoBehaviour;

        }

        public void GenerateNewProject()
        {
            var maxCountTasks = Enum.GetValues(typeof(TaskType)).Length;
            var randomCountTasks = Random.Range(5, maxCountTasks);
            this.project = new Project(randomCountTasks,monoBehaviour);
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