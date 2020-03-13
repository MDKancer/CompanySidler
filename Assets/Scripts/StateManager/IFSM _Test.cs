using System.Collections;
using Zenject_Signals;

namespace StateMachine
{
    public interface IFSM_Test
    {
        void OnEnter();
        IEnumerator OnUpdate();
        void OnExit();
    }
}