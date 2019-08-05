using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCloud
{
    public class Container  {

        private Dictionary<GameObject,int> sourcesGAmeObjects = new Dictionary<GameObject, int>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private Dictionary<KeyCode, int> allInputListenner = new Dictionary<KeyCode, int>();


        public void LoadAllResources()
        {
            
        }

        public void AddSpawnededGameObject()
        {
            
        }

        public List<GameObject> SpawnedGameObjects => spawnedGameObjects;

        public List<GameObject> GetPrefabByType()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            return gameObjects;
        }

        public Boolean isSpawned(GameObject gameObject) => spawnedGameObjects.Contains(gameObject);
    }
}
