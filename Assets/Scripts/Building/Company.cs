using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Customer.Data;
using Enums;
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
        private Dictionary<Project,CustomerType> companyProjects;
        private int projectCount;
        private readonly List<CustomerData> customers;
        public Company( GameObject company)
        {
            this.gameObject = company;
            this.name = gameObject.name;
            this.currentBudget = 0;
            GetAllInterfaces();
            offices = new Dictionary<Building, BuildingType>();
            customers = new List<CustomerData>();
            this.companyProjects = new Dictionary<Project,CustomerType>();
            SetCompanyData();
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

        public List<CustomerData> Customers => customers;

        public void AddNewProject(Project project, CustomerType customerType)
        {
            if(needProject && projectCount < projectLimit)
            {
                try
                {
                    companyProjects.Add(project,customerType);
                    projectCount++;
                }
                catch
                {
                    Debug.Log("Container for Projects ist full");
                    needProject = false;
                }
            }
        }

        public void RemoveProject(Project project)
        {
            if(companyProjects.ContainsKey(project))
            {
                companyProjects.Remove(project);
                projectCount--;
                needProject = true;
            }
        }

        public List<Project> GetProjectsByType(CustomerType customerType)
        {
            List<Project> returnList = new List<Project>();
            foreach (var item in companyProjects)
            {
                if (item.Value == customerType)
                {
                    returnList.Add(item.Key);
                }
            }

            return returnList;
        }
        
        private void SetCompanyData()
        {
            
            if (gameObject == null) return;
            
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var office = gameObject.transform.GetChild(i).GetComponent(typeof(Building)) as Building;
                if(office!=null)
                {
                    office.Company = this;
                    //nur am Anfang als test. als StartCapital
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
            if(buildingComponent.Length > 0)
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
        /// die sammelt alle Interfaces von alle Klasse die von Hauptklasse Building erben.
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