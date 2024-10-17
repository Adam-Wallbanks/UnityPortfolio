using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    string gameLobbyID;
    [SerializeField] private Button testBtn;

    // Start is called before the first frame update
    void Start()
    {
        testBtn.onClick.AddListener(printID);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void printID()
    {
        if (PlayerPrefs.HasKey("LobbyID"))
        {
            gameLobbyID = PlayerPrefs.GetString("LobbyID");
            Debug.Log(gameLobbyID);
        }
        else
        {
            gameLobbyID = "nothing";
        }
        
    }
}
