using System;
using System.Collections.Generic;
using System.Linq;
using Building.Accounting;
using Building.Administration;
using Building.Azubi;
using Building.DevOps;
using Building.Marketing;
using Building.Office;
using Building.Rewe;
using Building.Server;
using Building.SocialRoom;
using Building.Tarent;
using Building.Telekom;
using Building.Tom;
using Entity.Customer;
using Enums;
using ProjectPackage;
using UnityEngine;
using Zenject;
using Zenject.SceneContext.Signals;

namespace Building
{
    public class Company
    {
        private static Company currentCompany = null;
        public string name = null;
        public bool needProject = true;
        public int numberOfCustomers = 0;
        internal List<Type> sortedTypes = new List<Type>();
        
        private const int projectLimit = 12;
        private readonly GameObject gameObject = null;
        private int currentBudget;
        private Dictionary<Building,BuildingType> offices = null;
        /// <summary>
        /// All projects what the company have.
        /// </summary>
        private Dictionary<Project,CustomerType> companyProjects;
        private int projectCount;
        private readonly List<CustomerData> customers;
        private SignalBus signalBus;
        public Company(SignalBus signalBus,GameObject company)
        {
            this.signalBus = signalBus;
            this.gameObject = company;
            this.name = gameObject.name;
            this.currentBudget = 0;
            GetAllInterfaces();
            
            offices = new Dictionary<Building, BuildingType>();
            customers = new List<CustomerData>();
            this.companyProjects = new Dictionary<Project,CustomerType>();
            
            SetCompanyData();
        }

        public GameObject getCompanyGameObject() => gameObject;
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
//                currentBudget = 0;
//                foreach (var office in offices)
//                {
//                    currentBudget += office.Key.budget;
//                }
                return currentBudget;  
            } 
            set => currentBudget = value;
        }

        public List<CustomerData> Customers => customers;
        public List<Building> GetAllOffices => offices.Keys.ToList();
        public IList<Project> GetAllProjects => companyProjects.Keys.ToList().AsReadOnly();

        public void AddNewProject(Project project, CustomerType customerType)
        {
            if(needProject && projectCount < projectLimit)
            {
                try
                {
                    if (!companyProjects.ContainsValue(customerType))
                    {
                        numberOfCustomers++;
                    }
                    
                    if(!companyProjects.ContainsKey(project))
                    {
                        companyProjects.Add(project,customerType);
                        projectCount++;
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else if(projectCount >= projectLimit)
            {
                    needProject = false;
            }
        }

        public void RemoveProject(Project project)
        {
            if(companyProjects.ContainsKey(project))
            {
                var customer = companyProjects[project];
                companyProjects.Remove(project);
                projectCount--;
                needProject = true;

                if (!companyProjects.ContainsValue(customer))
                {
                    numberOfCustomers--;
                }
            }
        }

        public List<Project> GetProjectsIfExist(CustomerType customerType)
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
            
            
            signalBus.Fire(new CurrentCompanySignal
            {
                company = this
            });
            var summ = 0;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var office = gameObject.transform.GetChild(i).GetComponent(typeof(Building)) as Building;
                if(office!=null)
                {
                    // nur am Anfang als test. als StartCapital
                    // only on Begin as test, for the StartBudget
                     summ += 3300;
                    
                    offices.Add(office,GetBuildingType(office.gameObject));
                }
            }

            currentBudget = summ;
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
        /// Es ist eine Funktion die auf Assembly ebene funktioniert.
        /// die sammelt alle Interfaces von alle Klasse die von Hauptklasse Building erben.
        /// </summary>
        ///
        /// TODO: Vieleicht nicht alle Interfaces sondern Klassen die erben von Building!!!!
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