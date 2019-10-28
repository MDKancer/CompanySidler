using System;
using System.Collections.Generic;
using System.Linq;
using BuildingPackage;
using Enums;
using UnityEngine;
using Resources = UnityEngine.Resources;

 namespace GameCloud
{
    public class Container  {

        private List<Company> companies = new List<Company>();
        private Dictionary<GameObject,EntityType> prefabsObjects = new Dictionary<GameObject, EntityType>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private Dictionary<KeyCode, Actions> InputListenners = new Dictionary<KeyCode, Actions>();
        public void LoadAllResources()
        {
           //TODO: hier wird alle Prefabs aus den Ordnern im Dictionary reingepackt.
           
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Building"), EntityType.BUILDING);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Workers/Developers"), EntityType.DEVELOPER);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Azubis"), EntityType.AZUBI);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Clients"), EntityType.CLIENT);

            SetFirmaData();
        }

        public List<Company> Firmas
        {
            get => companies;
            private set => companies = value;
        }

        public void AddSpawnedGameObject(GameObject gameObject) => spawnedGameObjects.Add(gameObject);
    
        public  IList<GameObject> SpawnedGameObjects =>  spawnedGameObjects.AsReadOnly();
        public Boolean isSpawned(GameObject gameObject) => spawnedGameObjects.Contains(gameObject);

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

        private void addPrefabs(GameObject[] prefabs,EntityType entityType)
        {
            foreach (var prefab in prefabs)
            {
                if (!prefabsObjects.ContainsKey(prefab))
                {
                    prefabsObjects.Add(prefab, entityType);
                }
            }
        }
        private void SetFirmaData(string companyName = "Firma") => companies.Add(new Company(GameObject.Find(companyName)));

       

        
        
    }
}
