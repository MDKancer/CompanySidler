using System;
using System.Collections;
using Buildings;
using Entity.Customer;
using Entity.Employee;
using Enums;
using So_Template;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.ProjectContext.Signals;
using Object = UnityEngine.Object;

namespace SpawnManager
{
    public class SpawnController
    {
        private int index = 0;
        private SignalBus signalBus;
        private Container.Cloud cloud;
        private CompanyData companyData;
        private MonoBehaviour monoBehaviour;
        [Inject]
        private void Init(SignalBus signalBus,Container.Cloud cloud,CompanyData companyData,MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.companyData = companyData;
            this.monoBehaviour = monoBehaviourSignal;
        }
        public void InitialSpawnWave()
        {
            foreach (var officePrefab in companyData.basicOffices.offices)
            {
                var building = cloud.Companies[0].GetOffice(officePrefab);
                monoBehaviour.StartCoroutine(SpawnAfterInstancing(building));
            }
        }
        public void SpawnOffice(GameObject office, Vector3 targetPosition)
        {
            GameObject instance = Object.Instantiate(
                office,
                targetPosition,Quaternion.identity,
                cloud.Companies[0].getCompanyGameObject().transform);
            
            cloud.AddSpawnedGameObject(instance);
        }
        
        /// <summary>
        /// Diese Funktion Spawned ein Object in ein bestimmten Position.
        /// Nachdem wird es in Container gespeichert.
        /// </summary>
        /// <param name="employeeData"></param>
        /// <param name="spawnPosition"></param>
        /// <returns>Wenn das Object Instantiert wurde und in den Container gepseichert wurde, bekommt man zurrück ein true.</returns>
        public Boolean SpawnWorker(Building workerOffice,EmployeeData employeeData,Vector3 spawnPosition) //GameObject prefab, EntityType workerEntityType
        {
            try
            {
                var objectInstance = Object.Instantiate(employeeData.GetPrefab, spawnPosition, Quaternion.identity);
                if(objectInstance.GetComponent<NavMeshAgent>() == null)
                {
                    var agent = objectInstance.AddComponent<NavMeshAgent>();
                }
                if (objectInstance.GetComponent<Employee>() == null)
                {
                   var employee = objectInstance.AddComponent<Employee>();
                   
                   employee.EmployeeData = employeeData;
                   
                   employee.Work();
                }

                cloud.AddSpawnedGameObject(objectInstance);

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

                cloud.AddSpawnedGameObject(instantiate);
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
            var particleSystemObj = cloud.ParticleSystems[0];
            var company = cloud.Companies[0];

            var targetPosition =
                company.GetOffice(buildingType).transform.position + Vector3.up * 30;
            
            var particleSystem = GameObject.Instantiate(particleSystemObj, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();

            var particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            
            switch (particleType)
            {
                case ParticleType.CASH:
                    particleSystemRenderer.material = cloud.ParticleMaterials[0];
                    break;
                case ParticleType.PROJECT:
                    particleSystemRenderer.material = cloud.ParticleMaterials[1];
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
