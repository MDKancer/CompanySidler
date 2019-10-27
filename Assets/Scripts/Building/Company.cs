using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using ProjectPackage;
using UnityEngine;

namespace BuildingPackage
{
    public class Company
    {
        public string name = null;
        public bool needProject = true;
        
        internal List<Type> sortedTypes = new List<Type>();
        
        private const int projectLimit = 12;
        private GameObject gameObject = null;
        private int currentBudget;
        private Dictionary<Building,BuildingType> offices = null;
        private Dictionary<Project,ClientType> companyProjects;
        private int projectCount;
        public Company( GameObject company)
        {
            this.gameObject = company;
            this.name = gameObject.name;
            this.currentBudget = 0;
            GetAllInterfaces();
            offices = new Dictionary<Building, BuildingType>();
            this.companyProjects = new Dictionary<Project,ClientType>(projectLimit);
            SetFirmaData();
        }
        public Building GetOffice(BuildingType buildingType)
        {
            foreach (KeyValuePair<Building,BuildingType> office in offices)
            {
                if(office.Value == buildingType)
                {
                    return office.Key;
                }
            }
            return null;
        }

        public int CurrentBudget
        {
            get
            {
                currentBudget = 0;
                foreach (var office in offices)
                {
                    currentBudget += office.Key.budget;
                }
                return currentBudget;  
            } 
            protected set => currentBudget = value;
        }
        
        public void AddNewProject(Project project, ClientType clientType)
        {
            if(needProject)
            {
                companyProjects.Add(project,clientType);
                projectCount++;
            }
            if (companyProjects.Count >= projectLimit) needProject = false;
        }

        public void RemoveProject(Project project)
        {
            if(companyProjects.ContainsKey(project))
            {
                companyProjects.Remove(project);
                projectCount--;
            }
        }

        public List<Project> GetProjectsByType(ClientType clientType)
        {
            List<Project> returnList = new List<Project>();
            foreach (var item in companyProjects)
            {
                if (item.Value == clientType)
                {
                    returnList.Add(item.Key);
                }
            }

            return returnList;
        }
        
        private void SetFirmaData()
        {
            
            if (gameObject == null) return;
            
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var office = gameObject.transform.GetChild(i).GetComponent(typeof(Building)) as Building;
                if(office!=null)
                {
                    office.Company = this;
                    office.budget = 3300;
                    offices.Add(office,GetBuildingType(office.gameObject));
                }
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
            var buildingComponent = targetObjekt.GetComponents(typeof(iBuilding));
            var types = buildingComponent[0].GetType().GetInterfaces();
            if(buildingComponent.Length >0)
            {
                if (types.Contains(typeof(iOffice)))
                {
                    return BuildingType.OFFICE;
                }
                else if(types.Contains(typeof(iAccounting)))
                {
                    return BuildingType.ACCOUNTING;
                }
                else if (types.Contains(typeof(iSocialRoom)))
                {
                    return  BuildingType.SOCIAL_RAUM;
                }
                else if (types.Contains(typeof(iAdministration)))
                {
                    return  BuildingType.ADMIN;
                }
                else if (types.Contains(typeof(iDevOps)))
                {
                    return  BuildingType.DEV_OPS;
                }
                else if (types.Contains(typeof(iMarketing)))
                {
                    return  BuildingType.MARKETING;
                }
                else if (types.Contains(typeof(iRewe)))
                {
                    return  BuildingType.REWE;
                }
                else if (types.Contains(typeof(iTom)))
                {
                    return  BuildingType.TOM;
                }
                else if (types.Contains(typeof(iTelekom)))
                {
                    return  BuildingType.TELEKOM;
                }
                else if (types.Contains(typeof(iTarent)))
                {
                    return  BuildingType.TARENT_TOWN;
                }
                else if (types.Contains(typeof(iServer)))
                {
                    return  BuildingType.SERVER;
                }
                else if (types.Contains(typeof(iAzubis)))
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
            List<Type[]> unorderedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(Building).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.GetInterfaces()).ToList();
            
            for (int i = 0; i < unorderedTypes.Count; i++)
            {
                for (int j = 0; j < unorderedTypes[i].Length; j++)
                {
                    Type interfaceType = unorderedTypes[i][j];
                    if(!sortedTypes.Contains(interfaceType) && interfaceType != typeof(Building) )
                    {
                        sortedTypes.Add(interfaceType);
                    }
                }
            }
        }
    }
}