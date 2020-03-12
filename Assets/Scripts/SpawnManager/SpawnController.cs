﻿using System;
using System.Collections;
using BuildingPackage;
using Entity.Customer.Data;
using Enums;
using GameCloud;
using Human;
using Human.Customer;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject_Signals;
using Object = UnityEngine.Object;

namespace SpawnManager
{
    public class SpawnController
    {
        private int index = 0;
        private SignalBus signalBus;
        private Container container;
        private CompanyData companyData;
        private MonoBehaviour monoBehaviour;
        [Inject]
        private void Init(SignalBus signalBus,Container container,CompanyData companyData,MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.companyData = companyData;
            this.monoBehaviour = monoBehaviourSignal;
        }
        public void InitialSpawnWave()
        {
            foreach (var officePrefab in companyData.basicOffices.offices)
            {
                var building = container.Companies[0].GetOffice(officePrefab);
                monoBehaviour.StartCoroutine(SpawnAfterInstancing(building));
            }
        }
        public void SpawnOffice(GameObject office, Vector3 targetPosition)
        {
            GameObject instance = Object.Instantiate(
                office,
                targetPosition,Quaternion.identity,
                container.Companies[0].getCompanyGameObject().transform);
            
            container.AddSpawnedGameObject(instance);
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
                   //TODO: der Employee soll automatisch den Zugriff auf Container kriegen.
                   //Vieleicht die Company in Employeedata weitergeben, und nicht den Container.
                   employee.AttachEvent(workerOffice);
                   employee.Work();
                }

                container.AddSpawnedGameObject(objectInstace);

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

                container.AddSpawnedGameObject(instantiate);
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
            var particleSystemObj = container.ParticleSystems[0];
            var company = container.Companies[0];

            var targetPosition =
                company.GetOffice(buildingType).transform.position + Vector3.up * 30;
            
            var particleSystem = GameObject.Instantiate(particleSystemObj, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();

            var particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            
            switch (particleType)
            {
                case ParticleType.CASH:
                    particleSystemRenderer.material = container.ParticleMaterials[0];
                    break;
                case ParticleType.PROJECT:
                    particleSystemRenderer.material = container.ParticleMaterials[1];
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
