using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EntitiesISignals : MonoBehaviour
{
    [Range(0f, 18f)]
    [BoxGroup("Speed of Entities")]
    [HideLabel]
    public float entitiesSpeed = 3.5f;
        
    [Inject]
    private Container.Cloud cloud;

    public void Update()
    {
        if(cloud.SpawnedGameObjects.Count > 0)
        {
            foreach (var entity in cloud.SpawnedGameObjects)
            {
                if(entity != null && entity.GetComponent<NavMeshAgent>())
                {
                    entity.GetComponent<NavMeshAgent>().speed = entitiesSpeed;
                }
            }
        }
    }
        
        
    [HideLabel, ShowInInspector]
    [ProgressBar(0, 100, ColorMember = "GetStackedSpeedColor", BackgroundColorMember = "GetProgressBarBackgroundColor", DrawValueLabel = false)]
    [BoxGroup("Speed of Entities")]
    private float StackedHealthProgressBar
    {
        get { return this.entitiesSpeed % 100.01f; }
    }
        
    private Color GetStackedSpeedColor()
    {
        return
            this.entitiesSpeed > 15 ? Color.red :
            this.entitiesSpeed > 3 ? Color.green :
            Color.gray;
    }

    private Color GetProgressBarBackgroundColor()
    {
        return
            new Color(0.7f, 0.7f, 0.7f, 1f);
    }
    private Color GetBackgroundColor()
    {
        return
            new Color(0.7f, 0.7f, 0.7f, 1f);
    }
}