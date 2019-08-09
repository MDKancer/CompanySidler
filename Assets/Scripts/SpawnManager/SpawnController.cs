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

        public Boolean SpawnObject(GameObject go, Vector3 position)
        {
            try
            {
                GameObject gameObject = Object.Instantiate(go, position, Quaternion.identity);

                Boot.container.AddSpawnededGameObject(gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}
