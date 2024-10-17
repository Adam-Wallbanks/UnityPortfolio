using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float time;

    public TextMeshProUGUI timeDisplayText;

    public LobbyManager lobbyManager;

    // Start is called before the first frame update
    void Start()
    {
        updateTimerDisplayText(0);
        InvokeRepeating(nameof(updateTimeVal), 0.01f,0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateTimerDisplayText(int type)
    {
        int mins = Mathf.FloorToInt(time / 60);
        int secs = Mathf.FloorToInt(time % 60);
        if (type == 0)
        {
            string timeValStr = string.Format("Timer: {0:00}:{1:00}", mins, secs);
            timeDisplayText.text = timeValStr;
        }
        else
        {
            timeDisplayText.text = "View Chat For Scores";
        }
    }

    void updateTimeVal()
    {
        time -= 0.01f;

        if(time <= 0f)
        {
            lobbyManager.saveScores();
            CancelInvoke();
            updateTimerDisplayText(1);
        }
        else
        {
            updateTimerDisplayText(0);
        }
    }
}
