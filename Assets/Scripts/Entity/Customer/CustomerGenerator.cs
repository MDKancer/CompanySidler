using System.Collections;
using BootManager;
using UnityEngine;

namespace Human.Customer.Generator
{
    public class CustomerGenerator : MonoBehaviour
    {
        private readonly Vector3 spawnPosition = new Vector3(4f, 1f, 2f);


        private void Start()
        {
            StartCoroutine(CreateNewCostumer());
        }


        private IEnumerator CreateNewCostumer()
        {
            while (true)
            {
                if (Boot.container.Firmas[0].needProject)
                {
                    Boot.spawnController.SpawnCustomer(spawnPosition);
                    
                }
                yield return new WaitForSeconds(Random.Range(10f,15f));
            }
        }
    }
}