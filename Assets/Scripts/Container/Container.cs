﻿using System;
using System.Collections.Generic;
 using System.Linq;
 using BootManager;
 using BuildingPackage;
 using Constants;
 using Unity.Properties;
 using UnityEngine;
using Object = UnityEngine.Object;
 using Resources = UnityEngine.Resources;

 namespace GameCloud
{
    public class Container  {

        private Dictionary<GameObject,EntityType> prefabsObjects = new Dictionary<GameObject, EntityType>();
        private List<GameObject> spawnedGameObjects = new List<GameObject>();
        private Dictionary<KeyCode, Actions> InputListenners = new Dictionary<KeyCode, Actions>();
        private Dictionary<Vector3,BuildingType>  officePositions = new Dictionary<Vector3,BuildingType>();
        internal List<Type> sortedTypes = new List<Type>();
        public void LoadAllResources()
        {
           //TODO: hier wird alle Prefabs aus den Ordnern im Dictionary reingepackt.
           
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Building"), EntityType.BUILDING);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Workers"), EntityType.WORKER);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Azubis"), EntityType.AZUBI);
           addPrefabs(Resources.LoadAll<GameObject>("Prefabs/Entitys/Clients"), EntityType.CLIENT);

            GetAllInterfaces();
            SetOfficePosition();
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
        public Vector3 GetPositionOffice(BuildingType buildingType)
        {
            
            foreach (KeyValuePair<Vector3,BuildingType> item in officePositions)
            {
                if(item.Value == buildingType)
                {
                    return item.Key;
                }
            }
            return Vector3.zero;
        }

        public Boolean isSpawned(GameObject gameObject) => spawnedGameObjects.Contains(gameObject);


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

        private void SetOfficePosition()
        {
            GameObject firma = GameObject.Find("Firma");
            if (firma == null) return;
            for (int i = 0; i < firma.transform.childCount; i++)
            {
                officePositions.Add(firma.transform.GetChild(i).transform.position,GetBuildingType(firma.transform.GetChild(i).gameObject));
            }
        }

        /// <summary>
        /// Diese Funktion überrüft auf Assembler ebene,
        /// ob <param name="targetObjekt"></param> hat eine Commponente <para />
        /// was einer von Interfaces ( iOffice oder iAccouting etc...)   implementiert.<para />
        /// TODO : Weiter Fälle implementieren.!!!! 
        /// </summary>
        /// <param name="targetObjekt"></param>
        /// <returns></returns>
        private BuildingType GetBuildingType(GameObject targetObjekt)
        {
            Component[] buildingComponent = targetObjekt.GetComponents(typeof(iBuilding));
            if(buildingComponent.Length >0)
            {
                if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iOffice)))
                {
                    return BuildingType.OFFICE;
                }
                else if(buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iAccounting)))
                {
                    return BuildingType.ACCOUNTING;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iSocialRoom)))
                {
                    return  BuildingType.SOCIAL_RAUM;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iAdministration)))
                {
                    return  BuildingType.ADMIN;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iDevOps)))
                {
                    return  BuildingType.DEV_OPS;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iMarketing)))
                {
                    return  BuildingType.MARKETING;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iRewe)))
                {
                    return  BuildingType.REWE;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iTom)))
                {
                    return  BuildingType.TOM;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iTarent)))
                {
                    return  BuildingType.TARENT_TOWN;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iServer)))
                {
                    return  BuildingType.SERVER;
                }
                else if (buildingComponent[0].GetType().GetInterfaces().Contains(typeof(iAzubis)))
                {
                    return  BuildingType.AZUBIS;
                }
            }
            //TODO : Weiter Fälle implementieren.
            
            return BuildingType.NONE;
        }

        /// <summary>
        /// Es ist eine Funktion was funktioniert auf Assembly ebene.
        /// die sammelt alle Interfaces von alle Klasse die habe den Interface iBuilding implementiert.
        /// </summary>
        private void GetAllInterfaces()
        {
            List<Type[]> unornedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(iBuilding).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.GetInterfaces()).ToList();
            
            for (int i = 0; i < unornedTypes.Count; i++)
            {
                for (int j = 0; j < unornedTypes[i].Length; j++)
                {
                    Type interfaceType = unornedTypes[i][j];
                    if(!sortedTypes.Contains(interfaceType) && interfaceType != typeof(iBuilding) )
                    {
                        sortedTypes.Add(interfaceType);
                    }
                }
            }
        }
        
    }
}
