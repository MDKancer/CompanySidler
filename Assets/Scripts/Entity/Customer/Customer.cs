using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
using BuildingPackage;
using Constants;
using PathFinderManager;
using ProjectPackage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Human.Customer
{
    public class Customer : Human, iCustomer
    {
        public GameObject prefab;
        private Project project;
        private ClientType clientType;
        
        private void Awake()
        {
            prefab = Boot.container.GetPrefabsByType(EntityType.CLIENT)[0];
            this.destination = BuildingType.TARENT_TOWN;
            var randomTaskCount = Random.Range(1, 8);
            this.project = new Project(randomTaskCount);
            var rndIndex = Random.Range(0,Enum.GetValues(typeof(ClientType)).Length);
            clientType = (ClientType) Enum.GetValues(typeof(ClientType)).GetValue(rndIndex);
            project.clientType = clientType;
        }


        private void Start()
        {
            gameObject.name = clientType.ToString();
            StartCoroutine(ProjectTender());
        }

        private IEnumerator ProjectTender()
        {
            List<Company> firmas = Boot.container.Firmas;
            Vector3 initialPosition = gameObject.transform.position;
            TarentTown tarentTown = (TarentTown) firmas[0].GetOffice(destination);
            Vector3 tarentPosition = tarentTown.gameObject.transform.position;
            Vector3 targetPosition = GenerateRandomPosition(tarentPosition);
            
            PathFinder.MoveTo(gameObject,targetPosition);
            SelfState.CurrentState = HumanState.WORK;
            while (transform.position.x != targetPosition.x && transform.position.z != targetPosition.z )
            {
                // irgendwas
                yield return null;
            }
            
            // TODO : ist noch unter die Frage, wie soll ich das am besten machen?
            SelfState.CurrentState = HumanState.COMMUNICATION;
            tarentTown.TakeProject(project, clientType);

            SelfState.CurrentState = HumanState.QUITED;
            PathFinder.MoveTo(gameObject,initialPosition);
            while (transform.position.x != initialPosition.x && transform.position.z != initialPosition.z )
            {
                // irgendwas
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}