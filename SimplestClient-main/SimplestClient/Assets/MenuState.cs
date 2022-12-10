using UnityEngine;

public class MenuState : BaseStateMachine
{
    public override void EnterState(StateMachineManger state)
    {
        Debug.Log("Entering MenuState");
    }

    public override void UpdateState(StateMachineManger state)
    {
        if (true)
        {
            state.SwitchState(state.loginState);
        }
    }

    public override void ButtonPress(StateMachineManger state)
    {

    }
}
