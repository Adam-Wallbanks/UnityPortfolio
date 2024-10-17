using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickProfileScript : MonoBehaviour
{
    public Button profilePic1Select;
    public Button profilePic2Select;
    public Button profilePic3Select;
    public Button profilePic4Select;
    public Button profilePic5Select;
    public Button profilePic6Select;
    public Button profilePic7Select;
    public Button profilePic8Select;
    public List<Sprite> spriteList;
    public TextMeshProUGUI changeText;
    private SpriteRenderer spriteRenderer;
    public Button backBtn;

    private string playerId;

    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await VivoxService.Instance.LogoutAsync();

        spriteRenderer = GameObject.FindGameObjectWithTag("currentPic").GetComponent<SpriteRenderer>();

        if (PlayerPrefs.HasKey("ProfilePic"))
        {
            int profNum = PlayerPrefs.GetInt("ProfilePic");
            spriteRenderer.sprite = spriteList[profNum];
        }
        else
        {
            PlayerPrefs.SetInt("ProfilePic", 0);
        }

        profilePic1Select.onClick.AddListener(profilePic1Selected);
        profilePic2Select.onClick.AddListener(profilePic2Selected);
        profilePic3Select.onClick.AddListener(profilePic3Selected);
        profilePic4Select.onClick.AddListener(profilePic4Selected);
        profilePic5Select.onClick.AddListener(profilePic5Selected);
        profilePic6Select.onClick.AddListener(profilePic6Selected);
        profilePic7Select.onClick.AddListener(profilePic7Selected);
        profilePic8Select.onClick.AddListener(profilePic8Selected);
        backBtn.onClick.AddListener(onBackBtnPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void profilePic1Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 0);
        spriteRenderer.sprite = spriteList[0];
        StartCoroutine(showChangeText());
    }

    public void profilePic2Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 1);
        spriteRenderer.sprite = spriteList[1];
        StartCoroutine(showChangeText());
    }

    public void profilePic3Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 2);
        spriteRenderer.sprite = spriteList[2];
        StartCoroutine(showChangeText());
    }

    public void profilePic4Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 3);
        spriteRenderer.sprite = spriteList[3];
        StartCoroutine(showChangeText());
    }

    public void profilePic5Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 4);
        spriteRenderer.sprite = spriteList[4];
        StartCoroutine(showChangeText());
    }

    public void profilePic6Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 5);
        spriteRenderer.sprite = spriteList[5];
        StartCoroutine(showChangeText());
    }

    public void profilePic7Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 6);
        spriteRenderer.sprite = spriteList[6];
        StartCoroutine(showChangeText());
    }

    public void profilePic8Selected()
    {
        PlayerPrefs.SetInt("ProfilePic", 7);
        spriteRenderer.sprite = spriteList[7];
        StartCoroutine(showChangeText());
    }

    IEnumerator showChangeText()
    {
        changeText.text = "Your Profile Pic Has Been Changed Successfully";
        yield return new WaitForSeconds(1);
        changeText.text = "";
    }

    public void onBackBtnPress()
    {
        SceneManager.LoadScene("LobbyScreen");
    }

}
