using System.Threading.Tasks;
using UnityEngine;

namespace StateManager.States.EmploeeStates
{
    public class Work : EmployeeState
    {
        public override void OnEnter()
        {
            chore = GetActivity();
            if (chore != null)
            {
                chore.ApplyTaskTaker(emploee);
                        
                taskDuration = chore.TimeDuration;
                taskProgressBar = chore.ProgressBar;
                            
                time = 0f;
                part = taskProgressBar.howMuchNeed / (taskDuration / Time.smoothDeltaTime);
            }
        }

        public override void OnUpdate()
        {
            if (time <= taskDuration)
            {
                time += Time.smoothDeltaTime;
                taskProgressBar.percentDoneProgress += part;

                chore.ProgressBar = taskProgressBar;
                
                Task.Delay((int)Time.smoothDeltaTime);
            }
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
        
    }
}