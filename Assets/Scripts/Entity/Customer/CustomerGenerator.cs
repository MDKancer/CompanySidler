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
using Zenject_Signals;
using Random = UnityEngine.Random;

namespace Human.Customer.Generator
{
    public class CustomerGenerator : MonoBehaviour
    {
        private readonly Vector3 spawnPosition = new Vector3(4f, 1f, 2f);
        private Company company;
        
        private SignalBus signalBus;
        private Container container;
        private SpawnController spawnController;
        private MonoBehaviour monoBehaviour;

        [Inject]
        private void Init(SignalBus signalBus, Container container, SpawnController spawnController,MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviourSignal;
            
            this.signalBus.Subscribe<GameStateSignal>(StateDependency);
        }
        private void StateDependency(GameStateSignal gameStateSignal)
        {
            switch (gameStateSignal.state)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    company = container.Companies[0];
            
                    CreateCustomers();
                    StartCoroutine(CreateNewCostumer());
                    break;
                case GameState.EXIT:
                    break;
            }
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
                CustomerData customer = new CustomerData((CustomerType) customersValues.GetValue(i),spawnPosition,container,monoBehaviour);
                company.Customers.Add(customer);
            }
        }

        private void OnApplicationQuit()
        {
            signalBus.TryUnsubscribe<GameStateSignal>(StateDependency);
        }
        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<GameStateSignal>(StateDependency);
        }
    }
}