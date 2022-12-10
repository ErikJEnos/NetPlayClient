using UnityEngine;

public abstract class BaseStateMachine
{
    public abstract void EnterState(StateMachineManger state);

    public abstract void UpdateState(StateMachineManger state);

    public abstract void ButtonPress(StateMachineManger state);
}
