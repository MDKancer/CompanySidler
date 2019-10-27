using System.Collections;
using Constants;
using StateMachine;

namespace Human
{
    public interface IHuman
    {
        StateController<HumanState> SelfState { get; }
    }
}