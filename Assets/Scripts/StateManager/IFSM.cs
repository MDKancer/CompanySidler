using Zenject_Signals;

namespace StateMachine
{
    public interface IFSM
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}