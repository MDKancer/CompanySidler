using System;
using System.Collections.Generic;
using BootManager;
using BuildingPackage;
using Enums;
using StateMachine;
using UnityEngine;
using Zenject;
using Resources = UnityEngine.Resources;

 namespace GameCloud
{
    public class Container  {

        private List<Company> companies = new List<Company>();
        private Dictionary<GameObject,EntityType> prefabsObjects = new Dictionary<GameObject, EntityType>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private Dictionary<KeyCode, Actions> InputListenners = new Dictionary<KeyCode, Actions>();
        private List<Material> materials = new List<Material>();
        private List<Material> particleMaterials = new List<Material>();
        private List<GameObject> particleSystems = new List<GameObject>();
        [Inject]
        private StateController<GameState> gameStateController;
        public void LoadAllResources()
        {
           //TODO: hier wird alle Prefabs aus den Ordnern im Dictionary reingepackt.
           
           // wenn mann von Main Menu anfangen moechte.... dann auskommentieren.
          // if (Boot.gameStateController.CurrentState == GameState.INTRO)
           //{
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Buildings"), EntityType.BUILDING);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Offices"), EntityType.OFFICES);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Fortniture"), EntityType.FORNITURE);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Environment"), EntityType.ENVIRONMENT);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Workers/Developers"), EntityType.DEVELOPER);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Azubis"), EntityType.AZUBI);
               addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Clients"), EntityType.CUSTOMER);
               materials.AddRange(Resources.LoadAll<Material>("Materials"));
               particleMaterials.AddRange(Resources.LoadAll<Material>("Materials/Particle"));
               particleSystems.AddRange(Resources.LoadAll<GameObject>("Prefabs/ParticleSystems"));
           //}
        }

        public void SetDatas()
        {
            if(gameStateController.CurrentState == GameState.GAME)
            {
                SetCompanyData();
            }
        }

        public List<Material> Materials => materials;
        public List<Material> ParticleMaterials => particleMaterials;

        public List<GameObject> ParticleSystems => particleSystems;

        public List<Company> Companies
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
        private void SetCompanyData()
        {
            CompanyData companyData = Boot.boot_Instance.companyData;
            string companyName = companyData.nameCompany;
            
            GameObject company = GameObject.Find("Company");
            company.name = companyName;
            companies.Add(new Company(company));
        }
    }
}
