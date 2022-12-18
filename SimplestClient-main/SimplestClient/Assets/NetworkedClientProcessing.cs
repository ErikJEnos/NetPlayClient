using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkedClientProcessing
{
    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, int id)
    {
       
        Debug.Log("msg received = " + msg + ".");

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);
       
        string[] temp = msg.Split(',');
        int signifierID = int.Parse(temp[0]);

        if (signifierID == ServerToClientSignifiers.AccountComplete)
        {
            gameLogic.AccountCompleteF();
        }
        if (signifierID == ServerToClientSignifiers.AccountFailed)
        {
            gameLogic.AccountFailedF();
        }
        if (signifierID == ServerToClientSignifiers.LoginSuccessfull)
        {
            gameLogic.LoginSuccessfullF();
        }
        if (signifierID == ServerToClientSignifiers.LoginFailed)
        {
            gameLogic.LoginFailedF();
        }

        if (signifierID == ServerToClientSignifiers.WaitingForAnotherPlayer)
        {
            gameLogic.WaitingForAnotherPlayerF();
        }

        if (signifierID == ServerToClientSignifiers.EnterPlayState)
        {
            gameLogic.EnterPlayStateF();
        }

        if (signifierID == ServerToClientSignifiers.playTile)
        {
            gameLogic.playTileF(csv, id);
        }

        if (signifierID == ServerToClientSignifiers.Winner)
        {
            gameLogic.WinnerF();
        }

        if (signifierID == ServerToClientSignifiers.Loser)
        {

        }

        if (signifierID == ServerToClientSignifiers.ChatLogMessage)
        {
            gameLogic.ChatLogMessageF(csv, id);
        }

        if (signifierID == ServerToClientSignifiers.Replay)
        {
            gameLogic.ReplayF(csv, id);
        }


    }

    static public void SendMessageToServer(string msg)
    {
        networkedClient.SendMessageToServer(msg);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkedClient.IsConnected();
    }
    static public void ConnectToServer()
    {

        networkedClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkedClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkedClient networkedClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkedClient NetworkedClient)
    {
        networkedClient = NetworkedClient;
    }
    static public NetworkedClient GetNetworkedClient()
    {
        return networkedClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int BalloonClicked = 1;

}

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


#endregion
