using PathFinder;
using UnityEngine.AI;

namespace StateManager.States.EmployeeStates
{
    public class Quited : EmployeeState
    {
        public override void OnStateEnter()
        {
            navMeshAgent = employee.GetComponent<NavMeshAgent>();
            Navigator.MoveTo(navMeshAgent,EmployeeData.Home);
        }

        public override void OnStateUpdate()
        {
            if (IsOnPosition(EmployeeData.Home))
            {
                UnityEngine.Object.Destroy(employee.gameObject,0.1f);
                onCompleted.Invoke();
            }
        }

        public override void OnStateExit()
        {
            time = 0f;
        }
    }
}