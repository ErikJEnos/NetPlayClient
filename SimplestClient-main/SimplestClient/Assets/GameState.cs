using UnityEngine;

public class GameState : BaseStateMachine
{
    public override void EnterState(StateMachineManger state)
    {
        Debug.Log("Entering GameState");

        state.gameManager.GetComponent<TicTacToe>().GetButtons();
    }

    public override void UpdateState(StateMachineManger state)
    {

    }

    public override void ButtonPress(StateMachineManger state)
    {

    }
}
