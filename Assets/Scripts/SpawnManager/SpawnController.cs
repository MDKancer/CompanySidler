using System;
using BootManager;
using Constants;
using Life;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace SpawnManager
{
    public class SpawnController
    {
        /// <summary>
        /// TODO soll noch geschrieben werden!!!
        /// </summary>
        private int id = 0;
        public void InitialWaveSpawn()
        {
        }
        
        /// <summary>
        /// Diese Funktion Spawned ein Object in ein bestimmten Position.
        /// Nachdem wird es in Container gespeichert.
        /// </summary>
        /// <param name="prefab">
        /// <param name="position"></param>
        /// <returns>Wenn das Object Instantiert wurde und in den Container gepseichert wurde, bekommt man zurrück ein true.</returns>
        public Boolean SpawnObject(HumanData humanData,Vector3 spawnPosition) //GameObject prefab, EntityType workerEntityType
        {
            try
            {
                GameObject objectInstace = Object.Instantiate(humanData.GetPrefab, spawnPosition, Quaternion.identity);
                objectInstace.name = humanData.GetEntityType.ToString();
                if(objectInstace.GetComponent<NavMeshAgent>() == null)
                {
                    NavMeshAgent agent = objectInstace.AddComponent<NavMeshAgent>();
                }

                if (objectInstace.GetComponent<Worker>() == null)
                {
                   Worker worker = objectInstace.AddComponent<Worker>();
                   worker.HumanData = humanData;
                   worker.Work();
                }

                Boot.container.AddSpawnededGameObject(objectInstace);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
