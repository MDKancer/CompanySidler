using System;
using BootManager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpawnManager
{
    public class SpawnController
    {
        /// <summary>
        /// TODO soll noch geschrieben werden!!!
        /// </summary>
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
        public Boolean SpawnObject(GameObject prefab, Vector3 position)
        {
            try
            {
                GameObject objectInstace = Object.Instantiate(prefab, position, Quaternion.identity);

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
