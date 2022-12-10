using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkedClient : MonoBehaviour
{

    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5491;
    byte error;
    bool isConnected = false;
    int ourClientID;

    public GameObject gameManager;

    public int playerTurn = 1;

    public List<string> clientChatlog;

    public static class ServerToClientSignifiers
    {
        public const int AccountComplete = 01;
        public const int AccountFailed = 02;
        public const int LoginSuccessfull = 03;
        public const int LoginFailed = 04;
        public const int GameRoomSuccessfull = 05;
        public const int GameRoomFailed = 06;
        public const int WaitingForAnotherPlayer = 07;
        public const int EnterPlayState = 08;
        public const int playTile = 09;
        public const int Winner = 10;
        public const int Loser = 11;
        public const int LockPlayerControls = 12;
        public const int ChatLogMessage = 13;
        public const int Replay = 14;
    }

 

    // Start is called before the first frame update
    void Start()
    {
        Connect();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            SendMessageToHost("Hello from client");

        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }
    
    private void Connect()
    {

        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.0.15", socketPort, 0, out error); // server is local on network

            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }
    
    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }
    
    public void SendMessageToHost(string msg)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);


    }


    private void ProcessRecievedMsg(string msg, int id)
    {
        string[] temp = msg.Split(',');
        int signifierID = int.Parse(temp[0]);

        if (signifierID == ServerToClientSignifiers.AccountComplete)
        {
            Debug.Log("AccountComplete");
        }
        if (signifierID == ServerToClientSignifiers.AccountFailed)
        {
            Debug.Log("AccountFailed");
        }
        if (signifierID == ServerToClientSignifiers.LoginSuccessfull)
        {
            Debug.Log("LoginSuccessfull");
            gameManager.GetComponent<AccountLogin>().failed.SetActive(false);
            gameManager.GetComponent<AccountLogin>().MenuStatePanel.SetActive(false);
            gameManager.GetComponent<AccountLogin>().RoomSystemStatePanel.SetActive(true);
            gameManager.GetComponent<StateMachineManger>().loginState.ButtonPress(gameManager.GetComponent<StateMachineManger>());
        }
        if (signifierID == ServerToClientSignifiers.LoginFailed)
        {
            Debug.Log("LoginFailed");
            gameManager.GetComponent<AccountLogin>().failed.SetActive(true);
        }

        if (signifierID == ServerToClientSignifiers.WaitingForAnotherPlayer)
        {
            gameManager.GetComponent<GameRoom>().waitingForPlayerText.SetActive(true);
            gameManager.GetComponent<GameRoom>().buttonBack.SetActive(true);
        }

        if (signifierID == ServerToClientSignifiers.EnterPlayState)
        {
            gameManager.GetComponent<GameRoom>().GamePanel.SetActive(true);
            gameManager.GetComponent<GameRoom>().RoomPanel.SetActive(false);
            gameManager.GetComponent<StateMachineManger>().roomSystemState.ButtonPress(gameManager.GetComponent<StateMachineManger>());

            //SendMessageToHost(ServerToClientSignifiers.LockPlayerControls + ",");
        }

        if (signifierID == ServerToClientSignifiers.playTile)
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

        if (signifierID == ServerToClientSignifiers.Winner)
        {
            gameManager.GetComponent<TicTacToe>().winnerText.text = "YOU WIN";
        }

        if (signifierID == ServerToClientSignifiers.Loser)
        {
            //gameManager.GetComponent<TicTacToe>().winnerText.text = "YOU LOSE";
        }

        if (signifierID == ServerToClientSignifiers.ChatLogMessage)
        {
            clientChatlog.Add(temp[1]);
            //Debug.Log("Before wtriting chat: "+ temp[1].ToString());
            gameManager.GetComponent<TicTacToe>().chat.text = ""; 

            for (int x = 0; x < clientChatlog.Count; x++)
            {
                gameManager.GetComponent<TicTacToe>().chat.text += "Player: " + id + " -" + clientChatlog[x] + "\n";
            }

            Debug.Log(temp[0] + " : " + temp[1] + " : " + id);
        }

        if (signifierID == ServerToClientSignifiers.Replay)
        {
            for(int x = 0; x < gameManager.GetComponent<TicTacToe>().buttonList.Length; x++) 
            {
                gameManager.GetComponent<TicTacToe>().tiles[x].text = "";
                gameManager.GetComponent<TicTacToe>().tiles[x].transform.parent.GetComponent<Button>().interactable = true;
            }
        }

    }

    public bool IsConnected()
    {
        return isConnected;
    }



}
