using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCloud
{
    public class Container  {

        private Dictionary<GameObject,EntityType> prefabsObjects = new Dictionary<GameObject, EntityType>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private Dictionary<KeyCode, Actions> InputListenners = new Dictionary<KeyCode, Actions>();


        public void LoadAllResources()
        {
           //TODO: hier wird alle Prefabs aus den Ordnern im Dictionary reingepackt.
           
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Building"), EntityType.BUILDING);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Humans"), EntityType.HUMAN);
        }

        public void AddSpawnededGameObject(GameObject gameObject) => spawnedGameObjects.Add(gameObject);

        public  IList<GameObject> SpawnedGameObjects =>  spawnedGameObjects.AsReadOnly();

        public List<GameObject> GetPrefabsByType(EntityType entityType)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            foreach (KeyValuePair<GameObject,EntityType> item in prefabsObjects)
            {
                if(item.Value == entityType)
                {
                    gameObjects.Add(item.Key);
                }
            }
            return gameObjects;
        }

        public Boolean isSpawned(GameObject gameObject) => spawnedGameObjects.Contains(gameObject);


        private void addPrefabs(GameObject[] prefabs,EntityType entityType)
        {
            foreach (var prefab in prefabs)
            {
                prefabsObjects.Add(prefab, entityType);
            }
        }
    }
}
