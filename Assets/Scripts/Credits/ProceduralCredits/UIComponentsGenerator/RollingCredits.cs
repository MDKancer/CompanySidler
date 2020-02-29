using System.Collections;
using BootManager;
using UnityEngine;

namespace Credits
{
    public sealed class RollingCredits
    {
        public IEnumerator StartRolling(GameObject label)
        {
            GameObject lbl = label;
            RectTransform rT = lbl.GetComponent<RectTransform>();

            rT.localPosition = new Vector3(0, 0, 0);

            Vector3 v3_startPosition = new Vector3(0, 0, 0);
            Vector3 v3_endPosition = new Vector3(0, 130f, 0);
            float ts = 0;

            while (ts < 5.0f) // LifeTime
            {
                ts += Time.deltaTime / 5;
                
                if (rT != null)
                {
                    rT.localPosition = Vector3.Slerp(v3_startPosition, v3_endPosition, ts);

                    if (rT.localPosition == v3_endPosition)
                    {
                        Boot.boot_Instance.monoBehaviour.StopCoroutine(StartRolling(label));
                        Object.Destroy(label);
                    }
                }
                yield return ts;
            }
        }
    }
}