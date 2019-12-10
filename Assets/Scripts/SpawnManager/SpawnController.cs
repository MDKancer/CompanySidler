using System;
using System.Collections;
using System.Threading.Tasks;
using BootManager;
using BuildingPackage;
using Entity.Customer.Data;
using Enums;
using Human;
using Human.Customer;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace SpawnManager
{
    public class SpawnController
    {
        private int index = 0;
        public void InitialSpawnWave()
        {
            foreach (var officePrefab in Boot.boot_Instance.companyData.basicOffices.offices)
            {
                var building = Boot.container.Companies[0].GetOffice(officePrefab);
                Boot.monobehaviour.StartCoroutine(SpawnAfterInstancing(building));
            }
        }
        public void SpawnOffice(GameObject office, Vector3 targetPosition)
        {
            GameObject instance = Object.Instantiate(office,targetPosition,Quaternion.identity,Boot.container.Companies[0].getCompanyGameObject().transform);
            Boot.container.AddSpawnedGameObject(instance);
        }
        
        /// <summary>
        /// Diese Funktion Spawned ein Object in ein bestimmten Position.
        /// Nachdem wird es in Container gespeichert.
        /// </summary>
        /// <param name="employeeData"></param>
        /// <param name="spawnPosition"></param>
        /// <returns>Wenn das Object Instantiert wurde und in den Container gepseichert wurde, bekommt man zurrück ein true.</returns>
        public Boolean SpawnWorker(EmployeeData employeeData,Vector3 spawnPosition) //GameObject prefab, EntityType workerEntityType
        {
            try
            {
                GameObject objectInstace = Object.Instantiate(employeeData.GetPrefab, spawnPosition, Quaternion.identity);
                objectInstace.name = employeeData.GetEntityType.ToString();
                if(objectInstace.GetComponent<NavMeshAgent>() == null)
                {
                    NavMeshAgent agent = objectInstace.AddComponent<NavMeshAgent>();
                }

                if (objectInstace.GetComponent<Employee>() == null)
                {
                   Employee employee = objectInstace.AddComponent<Employee>();
                   employee.EmployeeData = employeeData;
                   employee.Work();
                }

                Boot.container.AddSpawnedGameObject(objectInstace);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public Boolean SpawnCustomer(ref CustomerData customerData,Vector3 spawnPosition)
        {
            try
            {
                GameObject instantiate = Object.Instantiate(customerData.prefab, spawnPosition, Quaternion.identity);

                if(instantiate.GetComponent<NavMeshAgent>() == null)
                {
                    NavMeshAgent agent = instantiate.AddComponent<NavMeshAgent>();
                    agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
//                    agent.radius = 1f;
                }

                if (instantiate.GetComponent<Employee>() == null)
                {
                   Customer customer =  instantiate.AddComponent<Customer>();
                   customer.customerData = customerData;
                }

                Boot.container.AddSpawnedGameObject(instantiate);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public ParticleSystem SpawnEffect(BuildingType buildingType,ParticleType particleType)
        {
            var particleSystemObj = Boot.container.ParticleSystems[0];
            var company = Boot.container.Companies[0];

            var targetPosition =
                company.GetOffice(buildingType).transform.position + Vector3.up * 30;
            
            var particleSystem = GameObject.Instantiate(particleSystemObj, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();

            var particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            
            switch (particleType)
            {
                case ParticleType.CASH:
                    particleSystemRenderer.material = Boot.container.ParticleMaterials[0];
                    break;
                case ParticleType.PROJECT:
                    particleSystemRenderer.material = Boot.container.ParticleMaterials[1];
                    break;
                default:
                    break;
            }
            particleSystem.Play();

            return particleSystem;
        }


        private IEnumerator SpawnAfterInstancing(Building building)
        {
            while (building.BuildingData.prefab == null)
            {
                yield return null;
            }
            building.IsBuying = true;
        }
    }
}
