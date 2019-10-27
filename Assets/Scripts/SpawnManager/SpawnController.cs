using System;
using BootManager;
using Constants;
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
        /// <param name="workerData"></param>
        /// <param name="spawnPosition"></param>
        /// <returns>Wenn das Object Instantiert wurde und in den Container gepseichert wurde, bekommt man zurrück ein true.</returns>
        public Boolean SpawnWorker(WorkerData workerData,Vector3 spawnPosition) //GameObject prefab, EntityType workerEntityType
        {
            try
            {
                GameObject objectInstace = Object.Instantiate(workerData.GetPrefab, spawnPosition, Quaternion.identity);
                objectInstace.name = workerData.GetEntityType.ToString();
                if(objectInstace.GetComponent<NavMeshAgent>() == null)
                {
                    NavMeshAgent agent = objectInstace.AddComponent<NavMeshAgent>();
                }

                if (objectInstace.GetComponent<Worker>() == null)
                {
                   Worker worker = objectInstace.AddComponent<Worker>();
                   worker.WorkerData = workerData;
                   worker.Work();
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

        public Boolean SpawnCustomer(Vector3 spawnPosition)
        {
            try
            {
                GameObject prefab = Boot.container.GetPrefabsByType(EntityType.CLIENT)[0];
                
                GameObject instantiate = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);

                if(instantiate.GetComponent<NavMeshAgent>() == null)
                {
                    NavMeshAgent agent = instantiate.AddComponent<NavMeshAgent>();
                }

                if (instantiate.GetComponent<Worker>() == null)
                {
                    instantiate.AddComponent<Customer>();
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
