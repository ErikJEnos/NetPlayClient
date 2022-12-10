using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using TMPro;


public class AccountLogin : MonoBehaviour
{
    public TMP_InputField username_Input;
    public TMP_InputField password_Input;
    public GameObject failed;
    public GameObject MenuStatePanel;
    public GameObject RoomSystemStatePanel;


    public bool login = false;
    public bool saved = false;
    public void SaveButtonPressed()
    {
        login = true;
    }

    public void LogonButtonPressed()
    {
        saved = true;
    }


  


}
