using UnityEngine;

public class LoginState : BaseStateMachine
{

    private string temp;
    public override void EnterState(StateMachineManger state)
    {
        Debug.Log("Entering LoginState");
    }

    public override void UpdateState(StateMachineManger state)
    {

        if(state.gameManager.GetComponent<AccountLogin>().login == true)
        {
            temp = state.gameManager.GetComponent<AccountLogin>().username_Input.text + "," + state.gameManager.GetComponent<AccountLogin>().password_Input.text;
           
            state.gameManager.GetComponent<NetworkedClient>().SendMessageToServer("1" + "," + temp);//createAccount           

            state.gameManager.GetComponent<AccountLogin>().login = false;
        }

        if (state.gameManager.GetComponent<AccountLogin>().saved == true)
        {
            temp = state.gameManager.GetComponent<AccountLogin>().username_Input.text + "," + state.gameManager.GetComponent<AccountLogin>().password_Input.text;

            state.gameManager.GetComponent<NetworkedClient>().SendMessageToServer("2" + "," + temp);//loginAccount

            state.gameManager.GetComponent<AccountLogin>().saved = false;
        }
    }

    public override void ButtonPress(StateMachineManger state)
    {
       
       state.SwitchState(state.roomSystemState);
       
    }
}
