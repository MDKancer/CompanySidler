using System;
using System.Collections;
using Entity.Customer.Data;
using Enums;
using PathFinderManager;
using TMPro;
using UnityEngine;

namespace Human.Customer
{
    public class Customer : Human, iCustomer
    {
        public CustomerData customerData;
        private TextMeshProUGUI namePoster;
        private Camera main;
        private void Awake()
        {
            main = Camera.main;
        }
        private void Start()
        {
            gameObject.name = customerData.customerType.ToString();
            StartCoroutine(ProjectTender());
            StartCoroutine(ShowMyNamePoster());
        }

        private IEnumerator ProjectTender()
        {

            Vector3 tarentPosition = customerData.TarentTownPosition();
            Vector3 targetPosition = GenerateRandomPosition(tarentPosition);
            
            PathFinder.MoveTo(gameObject,targetPosition);
            SelfState.CurrentState = HumanState.WORK;
            //-------------------------Move to Target------------------------
            // || || || || || || || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            while (transform.position.x != targetPosition.x && transform.position.z != targetPosition.z )
            {
//                Debug.Log("Name "+this.customerData.customerType+" "+transform.position + " Target " + tarentPosition);
                // irgendwas
                // hier passiert alles wärend des laufens den Kunden.
                if (PathFinder.MyPathStatus(gameObject) == PathProgress.NONE)
                {
                    targetPosition = GenerateRandomPosition(targetPosition);
                }
                yield return null;
            }
            
            // TODO : ist noch unter die Frage, wie soll ich das am besten machen?
            //-------------------------Communication-------------------------
            // || || || || || || || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            SelfState.CurrentState = HumanState.COMMUNICATION;
            
            customerData.SellProject();

            SelfState.CurrentState = HumanState.QUITED;
            PathFinder.MoveTo(gameObject,customerData.initialPosition);
            //-------------------------Return Back---------------------------
            // || || || || || || || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            while (transform.position.x != customerData.initialPosition.x && transform.position.z != customerData.initialPosition.z )
            {
                // irgendwas
                yield return null;
            }

            Destroy(gameObject);
        }
        private IEnumerator ShowMyNamePoster()
        {
            namePoster = uiElements.GetCanvas(this.customerData.customerType.ToString());
            
            RectTransform rectTransform = namePoster.GetComponent<RectTransform>();
            while (!gameObject.Equals(null))
            {
                rectTransform.position =
                    gameObject.transform.position + (gameObject.transform.up * 3f);
                rectTransform.rotation = Quaternion.LookRotation(main.transform.forward);
                yield return null;
            }
        }

        private void OnDestroy()
        {
            SelfState.CurrentState = HumanState.NONE;
            Destroy(namePoster);
        }
    }
}