using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BootManager;
using BuildingPackage;
using Life;
using Constants;
using InputManager;
using PathFinderManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Life
{
    public class Worker : Human, IAdmin
    {
        public HumanData HumanData
        {
            get => humanData;
            set => humanData = value;
        }
        private IEnumerator DO()
        {
            int index = 0;
            List<HumanState> myKeys = HumanData.GetEntityWorkingCycle.Keys.ToList();
            Debug.Log(myKeys.Count);
            Vector3 initialPosition = gameObject.transform.position;
            Vector3 officePosition = Boot.container.GetPositionOffice(HumanData.GetHisOffice);
            Vector3 targetPosition = GenerateRandomPosition(officePosition);
            
            destination = humanData.GetHisOffice;
            PathFinder.MoveTo(gameObject,targetPosition);
            
            while (SelfState.CurrentState != HumanState.QUITED)
            {
                if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
                {
                    yield return new WaitForSeconds(TimeToDo());

                    index = index >= myKeys.Count ? 0 : index;
                    
                    destination = HumanData.GetEntityWorkingCycle[myKeys[index]];
                    officePosition = Boot.container.GetPositionOffice(destination);
                    
                    SelfState.CurrentState = myKeys[index];
                    
                    targetPosition = GenerateRandomPosition(officePosition);
            
                    PathFinder.MoveTo(gameObject,targetPosition);
                    index++;
                }
                yield return null;
            }
            PathFinder.MoveTo(gameObject,initialPosition);
            yield return null;
        }

        public void Work()
        {
            Building.ApplyWorker(this);
            StartCoroutine(DO());
        }
        
        private iBuilding Building
        {
            get => InputController.FocusedBuilding?.GetComponent(typeof(iBuilding)) as iBuilding;
        }
    }
}