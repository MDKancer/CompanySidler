using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
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
    public class CustomerGenerator : MonoBehaviour
    {
        private readonly Vector3 spawnPosition = new Vector3(4f, 1f, 2f);
        private Company company;
        [Inject] private Container container;
        [Inject] private SpawnController spawnController;
        private void Awake()
        {
            
        }

        private void Start()
        {
            company = container.Companies[0];
            
            CreateCustomers();
            StartCoroutine(CreateNewCostumer());
        }


        private IEnumerator CreateNewCostumer()
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

        private void CreateCustomers()
        {
            var customersValues = Enum.GetValues(typeof(CustomerType));
            var countCustomers = customersValues.Length;
            
            for (int i = 0; i < countCustomers; i++)
            {
                CustomerData customer = new CustomerData((CustomerType) customersValues.GetValue(i),spawnPosition,container);
                company.Customers.Add(customer);
            }
        }
    }
}