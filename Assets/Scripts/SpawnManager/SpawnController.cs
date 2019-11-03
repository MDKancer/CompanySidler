using System;
using BootManager;
using Entity.Customer.Data;
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
        public void InitialWaveSpawn()
        {
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
    }
}
