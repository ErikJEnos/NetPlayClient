using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TicTacToe : MonoBehaviour
{
    public GameObject gameManager;
    public List<TMP_Text> tiles;
    public GameObject[] buttonList;
    public TMP_Text winnerText;
    public TMP_Text chat;
    public TMP_InputField chatInput;
    public bool won = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(won == false)
        {
            CheckBoard();
        }
       
        GetChatMessage();
    }

    public void Button1()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "0" + ",");
        

    }

    public void Button2()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "1" + ",");

    }

    public void Button3()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "2" + ",");

    }

    public void Button4()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "3" + ",");

    }

    public void Button5()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "4" + ",");

    }

    public void Button6()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "5" + ",");

    }

    public void Button7()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "6" + ",");

    }

    public void Button8()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "7" + ",");

    }

    public void Button9()
    {
        gameManager.GetComponent<NetworkedClient>().SendMessageToHost("5" + "," + "8" + ",");

    }


    public void CheckBoard()
    {
        if(tiles[0].text == "X" && tiles[1].text == "X" && tiles[2].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6"+","+"X");
            won = true;
        }

        else if (tiles[0].text == "O" && tiles[1].text == "O" && tiles[2].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[0].text == "X" && tiles[3].text == "X" && tiles[6].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if(tiles[0].text == "O" && tiles[3].text == "O" && tiles[6].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[1].text == "X" && tiles[4].text == "X" && tiles[7].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[1].text == "O" && tiles[4].text == "O" && tiles[7].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[2].text == "X" && tiles[5].text == "X" && tiles[8].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[2].text == "O" && tiles[5].text == "O" && tiles[8].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[3].text == "X" && tiles[4].text == "X" && tiles[5].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[3].text == "O" && tiles[4].text == "O" && tiles[5].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[6].text == "X" && tiles[7].text == "X" && tiles[8].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[6].text == "O" && tiles[7].text == "O" && tiles[8].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[0].text == "X" && tiles[4].text == "X" && tiles[8].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[0].text == "O" && tiles[4].text == "O" && tiles[8].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

        if (tiles[2].text == "X" && tiles[4].text == "X" && tiles[6].text == "X")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "X");
            won = true;
        }
        else if (tiles[2].text == "O" && tiles[4].text == "O" && tiles[6].text == "O")
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("6" + "," + "O");
            won = true;
        }

    }

    public void GetButtons()
    {
        buttonList = GameObject.FindGameObjectsWithTag("Button");
    }

    public void GetChatMessage()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.GetComponent<NetworkedClient>().SendMessageToHost("13" + "," + chatInput.text + ",");
            chatInput.text = "";
        }
    }
}
