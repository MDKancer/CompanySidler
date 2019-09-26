using System.Collections;
using BootManager;
using Constants;
using UnityEngine;

/// <summary>
/// Es ist nur eine Testklasse.
/// </summary>
public class WorkerGenerator : MonoBehaviour
{
     
    Vector3  spawnPosition = new Vector3(4f,1f,2f);

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWorkers());
    }

    private IEnumerator SpawnWorkers()
    {
        int workerID = 0;
        while (workerID <=100)
        {
           //Boot.spawnController.SpawnObject(prefab, spawnPosition, EntityType.AZUBI);
            workerID++;
            yield return new WaitForSeconds(2f);
        }
    }
}
