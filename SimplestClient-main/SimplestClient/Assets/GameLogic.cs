using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameLogic : MonoBehaviour
{

    LinkedList<int> connectedClientIDs;


    public GameObject gameManager;

    public int playerTurn = 1;

    public List<string> clientChatlog;

    // Start is called before the first frame update
    void Start()
    {
        NetworkedClientProcessing.SetGameLogic(this);
        gameManager = GameObject.Find("GameManager");

        connectedClientIDs = new LinkedList<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AccountCompleteF()
    {
        Debug.Log("AccountComplete");
    }
    public void AccountFailedF()
    {
        Debug.Log("AccountFailed");
    }
    public void LoginSuccessfullF()
    {
        Debug.Log("LoginSuccessfull");
        gameManager.GetComponent<AccountLogin>().failed.SetActive(false);
        gameManager.GetComponent<AccountLogin>().MenuStatePanel.SetActive(false);
        gameManager.GetComponent<AccountLogin>().RoomSystemStatePanel.SetActive(true);
        gameManager.GetComponent<StateMachineManger>().loginState.ButtonPress(gameManager.GetComponent<StateMachineManger>());

    }
    public void LoginFailedF()
    {
        Debug.Log("LoginFailed");
        gameManager.GetComponent<AccountLogin>().failed.SetActive(true);

    }
    public void WaitingForAnotherPlayerF()
    {
        gameManager.GetComponent<GameRoom>().waitingForPlayerText.SetActive(true);
        gameManager.GetComponent<GameRoom>().buttonBack.SetActive(true);
    }
    public void EnterPlayStateF()
    {
        gameManager.GetComponent<GameRoom>().GamePanel.SetActive(true);
        gameManager.GetComponent<GameRoom>().RoomPanel.SetActive(false);
        gameManager.GetComponent<StateMachineManger>().roomSystemState.ButtonPress(gameManager.GetComponent<StateMachineManger>());

    }
    public void playTileF(string[] temp, int id)
    {
        Debug.Log(temp[0] + " : " + temp[1] + " : " + temp[2]);
        if (id == 1)
        {
            gameManager.GetComponent<TicTacToe>().tiles[int.Parse(temp[2])].text = temp[1];
            gameManager.GetComponent<TicTacToe>().tiles[int.Parse(temp[2])].transform.parent.GetComponent<Button>().interactable = false;
        }
        else if (id == 2)
        {
            gameManager.GetComponent<TicTacToe>().tiles[int.Parse(temp[2])].text = temp[1];
            gameManager.GetComponent<TicTacToe>().tiles[int.Parse(temp[2])].transform.parent.GetComponent<Button>().interactable = false;
        }

    }
    public void WinnerF()
    {
        gameManager.GetComponent<TicTacToe>().winnerText.text = "YOU WIN";
    }

    public void ChatLogMessageF(string[] temp, int id)
    {
            clientChatlog.Add(temp[1]);
            //Debug.Log("Before wtriting chat: "+ temp[1].ToString());
            gameManager.GetComponent<TicTacToe>().chat.text = "";

            for (int x = 0; x <clientChatlog.Count; x++)
            {
                gameManager.GetComponent<TicTacToe>().chat.text += "Player: " + id + " -" + clientChatlog[x] + "\n";
            }

            Debug.Log(temp[0] + " : " + temp[1] + " : " + id);
    }

    public void ReplayF(string[] temp, int id)
    {

        for (int x = 0; x < gameManager.GetComponent<TicTacToe>().buttonList.Length; x++)
        {
           gameManager.GetComponent<TicTacToe>().tiles[x].text = "";
           gameManager.GetComponent<TicTacToe>().tiles[x].transform.parent.GetComponent<Button>().interactable = true;
        }
    }




}

