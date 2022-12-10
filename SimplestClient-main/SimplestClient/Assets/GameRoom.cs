using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameRoom : MonoBehaviour
{
    public TMP_InputField gameRoomName;
    public GameObject waitingForPlayerText;
    public GameObject buttonBack;
    public GameObject RoomPanel;
    public GameObject GamePanel;
    public bool createRoom = false;
    public bool back = false;

    public void createRoomButtonPressed()
    {
        createRoom = true;
    }

    public void createRoomBackButtonPressed()
    {
        back = true;
    }
}
