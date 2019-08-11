using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Credits
{
    public class CreditsManager : MonoBehaviour
    {

        public SoWorkers soCredits;
        public GameObject Parent;
        
        private LabelCreator lC = new LabelCreator();
        private RollingCredits rC = new RollingCredits();
        private GameObject title;
        private GameObject Label;
        private float dt = 2f; //abstand der labels
        private int index = 0;


        // Use this for initialization
        public void StartCredits()
        {

           title = lC.CreateTitle(soCredits.ProjektName, Parent);

            CreateLabel();
        }

        private void CreateLabel()
        {
            StartCoroutine(StartCredit());
        }

        private IEnumerator StartCredit()
        {
            float delayTime_ts = 0;
            while (delayTime_ts < dt)
            {
                delayTime_ts += Time.deltaTime;
                if (delayTime_ts > (dt * 0.8f))
                {

                    if (index < soCredits.Worker.Count)
                    {
                        Label = lC.CreateLabel(soCredits.Position[index], soCredits.Worker[index]);

                        Label.transform.SetParent(Parent.transform);

                        StartCoroutine(rC.StartRolling(Label));

                        index++;
                        delayTime_ts = 0;
                    }
                }

                yield return delayTime_ts;
            }

            yield return new WaitForSeconds(2f);
            Destroy(title);
            Label.transform.parent.gameObject.SetActive(false);
        }
    }
}