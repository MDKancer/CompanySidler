using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace Credits
{
    public class CreditsManager : MonoBehaviour
    {
        [Required]
        public SoWorkers soCredits;
        [Required]
        public GameObject Parent;
        
        private LabelCreator labelCreator;
        private RollingCredits rollingCredits ;
        private GameObject title;

        private SignalBus signalBus;
        private MonoBehaviour monoBehaviour;

        [Inject]
        private void Init(SignalBus signalBus,MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.monoBehaviour = monoBehaviourSignal;
            labelCreator = new LabelCreator();
            rollingCredits = new RollingCredits(monoBehaviour);
        }
        // Use this for initialization
        public void StartCredits()
        {
           title = labelCreator.CreateTitle(soCredits.ProjektName, Parent);

            CreateLabel();
        }

        private void CreateLabel()
        {
            StartCoroutine(StartCredit());
        }

        private IEnumerator StartCredit()
        {
            GameObject Label = null;
            float delayTime_ts = 0;
            int index=0;            
            
            while (delayTime_ts < 2.0f) //2.0f Delay der labels
            {
                delayTime_ts += Time.deltaTime;
                if (delayTime_ts > 1.6f)
                {
                    if (index < soCredits.Worker.Count)
                    {
                        Label = labelCreator.CreateLabel(soCredits.Position[index], soCredits.Worker[index]);

                        Label.transform.SetParent(Parent.transform);

                        StartCoroutine(rollingCredits.StartRolling(Label));

                        index++;
                        delayTime_ts = 0;
                    }
                }

                yield return delayTime_ts;
            }
            yield return new WaitForSeconds(2f);
            
            Destroy(title);
            Label?.transform.parent.gameObject.SetActive(false);
        }
    }
}