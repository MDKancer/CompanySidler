using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Enums;
using GameSettings;
using JSon_Manager;
using So_Template;
using StateManager;
using StateManager.State;
using StateManager.State.Template;
using UnityEngine;
using Zenject;
using Action = Enums.Action;
using Resources = UnityEngine.Resources;

 namespace Container
{
    public class Cloud  {

        private List<Company> companies = new List<Company>();
        private Dictionary<GameObject,EntityType> prefabsObjects = new Dictionary<GameObject, EntityType>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private SettingsData settingsData;
        private List<Material> materials = new List<Material>();
        private List<Material> particleMaterials = new List<Material>();
        private List<GameObject> particleSystems = new List<GameObject>();
        private Dictionary<Scenes,AState> gameStates = new Dictionary<Scenes, AState>();
        private Json_Stream jSon_manager = new Json_Stream("InputBindings.json");
        
        private SignalBus signalBus;
        private CompanyData companyData;
        
        [Inject]
        private void Init(SignalBus signalBus,CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.companyData = companyData;
        }
        /// <summary>
        /// Es wird ganz am anfang alle Prefabs aus den Ordnern im Dictionary reingepackt.
        /// It loaded all the stuff what will be need in the project.
        /// </summary>
        public void LoadAllResources()
        {
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Buildings"), EntityType.BUILDING);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Offices"), EntityType.OFFICES);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Fortniture"), EntityType.FORNITURE);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Environment"), EntityType.ENVIRONMENT);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Workers/Developers"), EntityType.DEVELOPER);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Azubis"), EntityType.AZUBI);
               AddPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Clients"), EntityType.CUSTOMER);
               materials.AddRange(Resources.LoadAll<Material>("Materials"));
               particleMaterials.AddRange(Resources.LoadAll<Material>("Materials/Particle"));
               particleSystems.AddRange(Resources.LoadAll<GameObject>("Prefabs/ParticleSystems"));
               
               
               //creat the instances for the game states
               gameStates.Add(Scenes.INTRO,new Intro());
               gameStates.Add(Scenes.MAIN_MENU,new MainMenu());
               gameStates.Add(Scenes.PREGAME,new PreGame());
               gameStates.Add(Scenes.GAME,new Game());
               gameStates.Add(Scenes.LOADING,new Loading());
               gameStates.Add(Scenes.EXIT,new Exit());
               
               LoadSettingsData();
        }
        /// <summary>
        /// Es wird ausgeführt wenn man alle wichtige Daten schon eingegeben hat um ein Unternehmen zu erstellen.
        /// It loaded/created all data what the company need.
        /// </summary>
        public void SetDatas()
        {
                SetCompanyData();
        }

        public List<Material> Materials => materials;
        public List<Material> ParticleMaterials => particleMaterials;
        public List<GameObject> ParticleSystems => particleSystems;
        public AudioData AudiosData => settingsData.audioData;
        public VideoData VideosData => settingsData.videoData;
        public KeyboardsData[] InputKeyboardData => settingsData.inputBindings;

        public List<Company> Companies
        {
            get => companies;
            private set => companies = value;
        }

        public void AddSpawnedGameObject(GameObject gameObject) => spawnedGameObjects.Add(gameObject);
    
        public  IList<GameObject> SpawnedGameObjects =>  spawnedGameObjects.AsReadOnly();
        public Boolean IsSpawned(GameObject gameObject) => spawnedGameObjects.Contains(gameObject);
        public IList<AState> GetGameStates => gameStates.Values.ToList().AsReadOnly();
        
        public AState GetGameState(Scenes scene) => gameStates[scene];
        
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
        public void SettingsDataReset()
        {
            settingsData.Reset();
            SaveSettingsData();
        }
        public void SaveSettingsData()
        {
            jSon_manager.WriteJson(settingsData);
        }
        private void AddPrefabs(GameObject[] prefabs,EntityType entityType)
        {
            foreach (var prefab in prefabs)
            {
                if (!prefabsObjects.ContainsKey(prefab))
                {
                    prefabsObjects.Add(prefab, entityType);
                }
            }
        }

        private void LoadSettingsData()
        {
             settingsData = jSon_manager.GetSettings<SettingsData>();
        }
        
        
        private void SetCompanyData()
        {
            GameObject company = GameObject.Find("Company");
            
            string companyName = companyData.nameCompany;
            
            company.name = companyName;
            companies.Add(new Company(signalBus,company));
        }
    }
}
