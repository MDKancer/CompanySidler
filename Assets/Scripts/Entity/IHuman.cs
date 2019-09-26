using System.Collections;
using Constants;
using StateMachine;

namespace Life
{
    public interface IHuman
    {
        StateController<HumanState> SelfState { get; }
    }
}