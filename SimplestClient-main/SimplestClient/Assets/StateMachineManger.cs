using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManger : MonoBehaviour
{
    BaseStateMachine currentState;
    public MenuState menuState = new MenuState();
    public LoginState loginState = new LoginState();
    public GameState gameState = new GameState();
    public RoomSystemState roomSystemState = new RoomSystemState();

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        currentState = menuState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseStateMachine state)
    {
        currentState = state;
        state.EnterState(this);

    }
}
