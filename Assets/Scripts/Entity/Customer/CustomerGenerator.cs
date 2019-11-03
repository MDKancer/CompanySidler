using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
using BuildingPackage;
using Entity.Customer.Data;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Human.Customer.Generator
{
    public class CustomerGenerator : MonoBehaviour
    {
        private readonly Vector3 spawnPosition = new Vector3(4f, 1f, 2f);
        private Company company;

        private void Awake()
        {
            company = Boot.container.Companies[0];
            CreateCustomers();
        }

        private void Start()
        {
            StartCoroutine(CreateNewCostumer());
        }


        private IEnumerator CreateNewCostumer()
        {
            while (true)
            {
                if (Boot.container.Companies[0].needProject)
                {
                    CustomerData customerData = company.Customers[Random.Range(0, company.Customers.Count - 1)];
                    customerData.GenerateNewProject();
                    Boot.spawnController.SpawnCustomer(ref customerData,spawnPosition);

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
                CustomerData customer = new CustomerData((CustomerType) customersValues.GetValue(i),spawnPosition);
                company.Customers.Add(customer);
            }
        }
    }
}