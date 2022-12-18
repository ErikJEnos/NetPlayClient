using UnityEngine;

public class RoomSystemState : BaseStateMachine
{

    private string temp;
    public override void EnterState(StateMachineManger state)
    {
        Debug.Log("Entering RoomSystemState");
    }

    public override void UpdateState(StateMachineManger state)
    {
        if (state.gameManager.GetComponent<GameRoom>().createRoom == true)
        {
            temp = state.gameManager.GetComponent<GameRoom>().gameRoomName.text;
            state.gameManager.GetComponent<NetworkedClient>().SendMessageToServer("3" + "," + temp);
            state.gameManager.GetComponent<GameRoom>().createRoom = false;
        }

        if(state.gameManager.GetComponent<GameRoom>().back == true)
        {
            temp = state.gameManager.GetComponent<GameRoom>().gameRoomName.text;
            state.gameManager.GetComponent<NetworkedClient>().SendMessageToServer("4" + "," + temp);
            state.gameManager.GetComponent<GameRoom>().back = false;
            state.gameManager.GetComponent<GameRoom>().waitingForPlayerText.SetActive(false);
        }
      
    }

    public override void ButtonPress(StateMachineManger state)
    {
        state.SwitchState(state.gameState);
    }
}
