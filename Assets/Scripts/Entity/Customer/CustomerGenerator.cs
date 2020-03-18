using System;
using System.Collections;
using BuildingPackage;
using Entity.Customer.Data;
using Enums;
using GameCloud;
using SpawnManager;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Human.Customer.Generator
{
    public class CustomerGenerator
    {
        public readonly Vector3 spawnPosition = new Vector3(4f, 1f, 2f);
        private Company company;
        
        private SignalBus signalBus;
        private Container container;
        private SpawnController spawnController;
        private MonoBehaviour monoBehaviour;

        public void Init(SignalBus signalBus, Container container, SpawnController spawnController,MonoBehaviour monoBehaviour)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            company = this.container.Companies[0];
        }

        public void Awake()
        {
            //CreateCustomers();
        }

        public void Start()
        {
            //StartCoroutine(CreateNewCostumer());
        }

        /// <summary>
        /// for each spawned customers is one project generated. 
        /// </summary>
        public IEnumerator CreateNewCostumer()
        {
            while (true)
            {
                if (container.Companies[0].needProject)
                {
                    CustomerData customerData = company.Customers[Random.Range(0, company.Customers.Count - 1)];
                    customerData.GenerateNewProject();
                    spawnController.SpawnCustomer(ref customerData,spawnPosition);
                }
                yield return new WaitForSeconds(Random.Range(10f,15f));
            }
        }
        /// <summary>
        /// create the template costumers.
        /// </summary>
        public void CreateCustomers()
        {
            
            
            var customersValues = Enum.GetValues(typeof(CustomerType));
            var countCustomers = customersValues.Length;
            
            for (int i = 0; i < countCustomers; i++)
            {
                CustomerData customer = new CustomerData((CustomerType) customersValues.GetValue(i),spawnPosition,container,monoBehaviour);
                company.Customers.Add(customer);
            }
        }
    }
}