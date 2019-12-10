using System.Collections;
using Enums;
using StateMachine;

namespace Human
{
    public interface IHuman
    {
        StateController<HumanState> SelfState { get; }
    }
}