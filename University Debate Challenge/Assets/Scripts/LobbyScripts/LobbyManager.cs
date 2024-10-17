using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Vivox;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Threading.Tasks;

public class LobbyManager : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Button getLobbiesListBtn;
    [SerializeField] private GameObject lobbyInfoPrefab;
    [SerializeField] private GameObject lobbiesInfoContent;
    [SerializeField] private TMP_InputField playerNameIF;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button backToMainMenuBtn;

    [Space(10)]
    [Header("Create Lobby Panel")]
    [SerializeField] private GameObject createRoomPanel;
    [SerializeField] private TMP_InputField roomNameIF;
    [SerializeField] private TMP_InputField maxPlayersIF;
    [SerializeField] private Button createRoomBtn;
    [SerializeField] private Toggle isPrivateToggle;


    [Space(10)]
    [Header("Lobby Panel")]
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private GameObject playerInfoContent;
    [SerializeField] private GameObject playerInfoPrefab;
    [SerializeField] private Button leaveRoomButton;
    [SerializeField] private Button startGameButton;


    [Space(10)]
    [Header("Join Lobby With Code Panel")]
    [SerializeField] private GameObject joinRoomPanel;
    [SerializeField] private TMP_InputField roomCodeIF;
    [SerializeField] private Button joinRoomBtn;

    [Space(10)]
    [Header("Game Panel")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject playerContent;
    [SerializeField] private GameObject playerprefab;
    [SerializeField] private Button openChatBtn;
    [SerializeField] private Button leaveGameBtn;
    [SerializeField] private Button infoBtn;
    [SerializeField] private GameObject timerObject;

    [Space(10)]
    [Header("Chat Panel")]
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TMP_InputField chatIF;
    [SerializeField] private GameObject chatInfoContent;
    [SerializeField] private Button sendChatBtn;
    [SerializeField] private GameObject chatInfoPrefab;
    [SerializeField] private Button chatBackBtn;
    [SerializeField] private TextMeshProUGUI topicText;

    [Space(10)]
    [Header("Sprite List")]
    [SerializeField] private List<Sprite> profilePicList;

    [Space(10)]
    [Header("Topic Picker Panel")]
    [SerializeField] private GameObject topicPickPanel;
    [SerializeField] private Button Topic1;
    [SerializeField] private Button Topic2;
    [SerializeField] private Button Topic3;
    [SerializeField] private List<string> topics;

    [Space(10)]
    [Header("Leaderboard Panel")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private GameObject scoreInfoContent;

    [Space(10)]
    [Header("Role Panels")]
    [SerializeField] private GameObject debateMasterPanel;
    [SerializeField] private GameObject debateParticipantPanel;

    [Space(10)]
    [Header("Information Panels")]
    [SerializeField] private GameObject debateMasterInfoPanel;
    [SerializeField] private GameObject debateParticipantInfoPanel;
    [SerializeField] private Button debateMasterBackBtn;
    [SerializeField] private Button debateParticipantBackBtn;

    private List<string> usedTopics = new List<string>();
    private Lobby currentLobby;
    public string lobbyID;
    private string playerId;
    private SpriteRenderer profileRenderer;
    private string currentTopic;
    // Start is called before the first frame update
    async void Start()
    {


        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log("Signed in " + playerId);
        };
        if (AuthenticationService.Instance.IsSignedIn)
        {

        }
        else
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        await VivoxService.Instance.InitializeAsync();

        // Log in to Vivox services
        await VivoxService.Instance.LoginAsync();

        createRoomBtn.onClick.AddListener(CreateLobby);
        joinRoomBtn.onClick.AddListener(JoinLobbyWithCode);
        getLobbiesListBtn.onClick.AddListener(ListPublicLobbies);
        sendChatBtn.onClick.AddListener(createChat);
        openChatBtn.onClick.AddListener(openChat);
        profileButton.onClick.AddListener(onProfileButtonPressAsync);
        chatBackBtn.onClick.AddListener(chatBack);
        profileRenderer = GameObject.FindGameObjectWithTag("currentPic").GetComponent<SpriteRenderer>();
        Topic1.onClick.AddListener(onTopicBtn1Pressed);
        Topic2.onClick.AddListener(onTopicBtn2Pressed);
        Topic3.onClick.AddListener(onTopicBtn3Pressed);

        if (PlayerPrefs.HasKey("CustomTopics"))
        {
            topics = getTopicsList();
        }

        if (PlayerPrefs.HasKey("ProfilePic"))
        {
            int profNum = PlayerPrefs.GetInt("ProfilePic");
            profileRenderer.sprite = profilePicList[profNum];
        }
        else
        {
            PlayerPrefs.SetInt("ProfilePic", 0);
            profileRenderer.sprite = profilePicList[0];
        }

        EventTrigger trigger = chatIF.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Submit;
        entry.callback.AddListener((data) => createChat());
        trigger.triggers.Add(entry);

        playerNameIF.onValueChanged.AddListener(delegate
        {
            PlayerPrefs.SetString("Name", playerNameIF.text);
        });

        playerNameIF.text = PlayerPrefs.GetString("Name");

        leaveRoomButton.onClick.AddListener(LeaveRoom);
        leaveGameBtn.onClick.AddListener(LeaveRoom);
        infoBtn.onClick.AddListener(onInfoBtnPress);
        debateParticipantBackBtn.onClick.AddListener(onBackBtnDebateParticipantInfoPanel);
        debateMasterBackBtn.onClick.AddListener(onBackBtnDebateMasterInfo);
        backToMainMenuBtn.onClick.AddListener(backToMainMenuAsync);
    }

    // Update is called once per frame
    void Update()
    {
        HandleLobbiesListUpdate();
        HandleLobbyHeartbeat();
        HandleRoomUpdate();
    }


    private async void CreateLobby()
    {
        try
        {
            string lobbyName = roomNameIF.text;
            int.TryParse(maxPlayersIF.text, out int maxPlayers);
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = isPrivateToggle.isOn,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"IsGameStarted", new DataObject(DataObject.VisibilityOptions.Member,"false") }
                }
            };
            currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            await VivoxService.Instance.JoinEchoChannelAsync(currentLobby.Id, ChatCapability.TextOnly);
            EnterRoom();
            Debug.Log("entering room");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    private async void createChat()
    {
        string chat = chatIF.text;
        //GameObject chatObj = Instantiate(chatInfoPrefab, chatInfoContent.transform);
        //chatObj.GetComponentInChildren<TextMeshProUGUI>().text = chat;
        if (string.IsNullOrEmpty(chatIF.text))
        {
            return;
        }
        await VivoxService.Instance.SendChannelTextMessageAsync(currentLobby.Id, chat);
        chatIF.text = string.Empty;
        Debug.Log("chat created");
    }


    void OnChannelMessageReceived(VivoxMessage message)
    {
        var channelName = message.ChannelName;
        var senderName = message.SenderDisplayName;
        var senderId = message.SenderPlayerId;
        var messageText = message.MessageText;
        var timeReceived = message.ReceivedTime;
        var language = message.Language;
        var fromSelf = message.FromSelf;
        var messageId = message.MessageId;
    }


    private async void visualizeChats() {
        VivoxService.Instance.ChannelMessageReceived += OnChannelMessageReceived;
        var historyMessages = await VivoxService.Instance.GetChannelTextMessageHistoryAsync(currentLobby.Id, 25);
        for (int i = 0; i < chatInfoContent.transform.childCount; i++)
        {
            Destroy(chatInfoContent.transform.GetChild(i).gameObject);
        }
        if (IsinLobby()) {
            //Reversing the messages so they display from oldest to newest
            var reversedMessages = historyMessages;
            foreach (VivoxMessage message in reversedMessages)
            {
                GameObject newChatInfo = Instantiate(chatInfoPrefab, chatInfoContent.transform);
                var chatDetails = newChatInfo.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (Player player in currentLobby.Players)
                {
                    if (player.Id == message.SenderPlayerId)
                    {
                        chatDetails[0].text = player.Data["PlayerName"].Value;
                        chatDetails[1].text = message.MessageText;
                    }
                }
                foreach (TextMeshProUGUI text in chatDetails)
                {
                    text.color = Color.white;
                }
            }
        }

    }




    private async void EnterRoom()
    {
        Debug.Log("room entered");
        mainMenuPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        joinRoomPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = currentLobby.Name;
        roomCode.text = currentLobby.LobbyCode;
        PlayerPrefs.SetString("LobbyID", currentLobby.Id);
        Debug.Log(PlayerPrefs.GetString("LobbyID"));
        VisualizeRoomDetails();
        var previousScoreMessages = await VivoxService.Instance.GetDirectTextMessageHistoryAsync(currentLobby.Id, 50);
        foreach (VivoxMessage scoreMessage in previousScoreMessages)
        {
            await VivoxService.Instance.DeleteDirectTextMessageAsync(scoreMessage.MessageId);
        }
    }

    private float roomUpdateTimer = 2f;
    private async void HandleRoomUpdate()
    {
        if (currentLobby != null)
        {
            roomUpdateTimer -= Time.deltaTime;
            if (roomUpdateTimer <= 0)
            {
                roomUpdateTimer = 2f;
                try
                {
                    if (IsinLobby())
                    {
                        currentLobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                        VisualizeRoomDetails();
                        visualizeChats();
                    }

                }
                catch (LobbyServiceException e)
                {
                    Debug.Log(e);
                    if ((e.Reason == LobbyExceptionReason.Forbidden || e.Reason == LobbyExceptionReason.LobbyNotFound))
                    {
                        currentLobby = null;
                        ExitRoom();
                    }
                }
            }
        }

    }

    private bool IsinLobby()
    {
        foreach (Player _player in currentLobby.Players)
        {
            if (_player.Id == playerId)
            {
                return true;
            }
        }
        currentLobby = null;
        return false;
    }

    private void VisualizeRoomDetails()
    {
        for (int i = 0; i < playerInfoContent.transform.childCount; i++)
        {
            Destroy(playerInfoContent.transform.GetChild(i).gameObject);
        }
        if (IsinLobby())
        {
            foreach (Player player in currentLobby.Players)
            {
                GameObject newPlayerInfo = Instantiate(playerInfoPrefab, playerInfoContent.transform);
                newPlayerInfo.GetComponentInChildren<TextMeshProUGUI>().text = player.Data["PlayerName"].Value;
                if (IsHost() && player.Id != playerId)
                {
                    Button kickBtn = newPlayerInfo.GetComponentInChildren<Button>(true);
                    kickBtn.onClick.AddListener(() => KickPlayer(player.Id));
                    kickBtn.gameObject.SetActive(true);

                }
            }

            if (IsHost())
            {
                startGameButton.onClick.AddListener(StartGame);
                startGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start Game";
                startGameButton.gameObject.SetActive(true);
            }
            else
            {
                if (IsGameStarted())
                {
                    startGameButton.onClick.AddListener(EnterGame);
                    startGameButton.gameObject.SetActive(true);
                    startGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enter Game";
                }
                else
                {
                    startGameButton.onClick.RemoveAllListeners();
                    startGameButton.gameObject.SetActive(false);
                }
            }

        }
        else
        {
            ExitRoom();
        }

    }

    private async void ListPublicLobbies()
    {
        try
        {
            QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync();
            VisualizeLobbyList(response.Results);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }


    private float updateLobbiesListTimer = 2f;

    private void HandleLobbiesListUpdate()
    {
        updateLobbiesListTimer -= Time.deltaTime;
        if (updateLobbiesListTimer <= 0)
        {
            ListPublicLobbies();
            updateLobbiesListTimer = 2f;
        }
    }


    private void VisualizeLobbyList(List<Lobby> _publicLobbies)
    {
        // We need to clear previous info
        for (int i = 0; i < lobbiesInfoContent.transform.childCount; i++)
        {
            Destroy(lobbiesInfoContent.transform.GetChild(i).gameObject);
        }
        foreach (Lobby _lobby in _publicLobbies)
        {
            GameObject newLobbyInfo = Instantiate(lobbyInfoPrefab, lobbiesInfoContent.transform);
            var lobbyDetailsTexts = newLobbyInfo.GetComponentsInChildren<TextMeshProUGUI>();
            lobbyDetailsTexts[0].text = _lobby.Name;
            lobbyDetailsTexts[1].text = (_lobby.MaxPlayers - _lobby.AvailableSlots).ToString() + "/" + _lobby.MaxPlayers.ToString();
            foreach (TextMeshProUGUI text in lobbyDetailsTexts)
            {
                text.color = Color.white;
            }
            newLobbyInfo.GetComponentInChildren<Button>().onClick.AddListener(() => JoinLobby(_lobby.Id)); // We will call join lobby 
        }
    }

    private async void JoinLobby(string _lobbyId)
    {
        try
        {
            JoinLobbyByIdOptions options = new JoinLobbyByIdOptions
            {
                Player = GetPlayer()
            };
            currentLobby = await LobbyService.Instance.JoinLobbyByIdAsync(_lobbyId, options);
            await VivoxService.Instance.JoinEchoChannelAsync(currentLobby.Id, ChatCapability.TextOnly);
            EnterRoom();
            Debug.Log("Players in room : " + currentLobby.Players.Count);
        } catch (LobbyServiceException e)
        {
            Debug.Log(e.ErrorCode);

        }
    }


    private async void JoinLobbyWithCode()
    {
        string _lobbyCode = roomCodeIF.text;
        try
        {
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            currentLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(_lobbyCode, options);
            await VivoxService.Instance.JoinEchoChannelAsync(currentLobby.Id, ChatCapability.TextOnly);
            EnterRoom();
            Debug.Log("Players in room : " + currentLobby.Players.Count);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }




    private float heartbeattimer = 15f;
    private async void HandleLobbyHeartbeat()
    {
        if (currentLobby != null && IsHost())
        {
            heartbeattimer -= Time.deltaTime;
            if (heartbeattimer <= 0)
            {
                heartbeattimer = 15f;
                await LobbyService.Instance.SendHeartbeatPingAsync(currentLobby.Id);
            }
        }
    }

    private bool IsHost()
    {
        if (currentLobby != null && currentLobby.HostId == playerId)
        {
            return true;
        }
        return false;
    }


    private Player GetPlayer()
    {
        string playerName = PlayerPrefs.GetString("Name");
        if (playerName == null || playerName == "")
            playerName = playerId;
        Player player = new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                {"PlayerName",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member,playerName) }
            }
        };

        return player;
    }


    private async void LeaveRoom()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, playerId);
            await VivoxService.Instance.LeaveAllChannelsAsync();
            currentLobby = null;
            TimerScript timer = new TimerScript();
            timer.time = 120;
            ExitRoom();
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private async void KickPlayer(string _playerId)
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, _playerId);
        } catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private void ExitRoom()
    {
        mainMenuPanel.SetActive(true);
        createRoomPanel.SetActive(false);
        joinRoomPanel.SetActive(false);
        roomPanel.SetActive(false);
        gamePanel.SetActive(false);
    }


    private async void StartGame()
    {
        if (currentLobby != null && IsHost())
        {
            try
            {
                UpdateLobbyOptions updateoptions = new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                         {"IsGameStarted", new DataObject(DataObject.VisibilityOptions.Member,"true") }
                    }
                };

                currentLobby = await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, updateoptions);

                EnterGame();

            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }

        }

    }



    private bool IsGameStarted()
    {
        if (currentLobby != null)
        {
            if (currentLobby.Data["IsGameStarted"].Value == "true")
            {
                return true;
            }
        }
        return false;
    }


    private void EnterGame()
    {
        mainMenuPanel.SetActive(false);
        roomPanel.SetActive(false);
        Debug.Log("opening game screen");
        displayPlayers();
        topicPickDisplay();
        
    }

    private void openChat() {
        chatPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    public string getLobbyID()
    {
        return currentLobby.Id;
    }

    public void displayPlayers() {
        for (int i = 0; i < playerContent.transform.childCount; i++)
        {
            Destroy(playerContent.transform.GetChild(i).gameObject);
        }
        foreach (Player player in currentLobby.Players)
        {
            GameObject newPlayerInfo = Instantiate(playerprefab, playerContent.transform);
            TextMeshProUGUI[] texts = newPlayerInfo.GetComponentsInChildren<TextMeshProUGUI>();
            Button[] buttons = newPlayerInfo.GetComponentsInChildren<Button>();
            foreach(Button button in buttons)
            {
                if (button.tag == "PlusButton")
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = player.Id;
                    button.onClick.AddListener(() => onPlusButtonPress(button,newPlayerInfo));
                    button.gameObject.SetActive(IsHost());
                }
                else if (button.tag == "MinusButton") 
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = player.Id;
                    button.onClick.AddListener(() => onMinusButtonPress(button,newPlayerInfo));
                    button.gameObject.SetActive(IsHost());
                }
                else
                {
                    button.GetComponent<Image>().sprite = profilePicList[PlayerPrefs.GetInt("ProfilePic")]; 
                }
            }
            foreach(TextMeshProUGUI text in texts)
            {
                if(text.tag == "Name")
                {
                    text.text = player.Data["PlayerName"].Value;
                }
                if(text.tag == "Score")
                {
                    if (IsHost())
                    {
                        text.text = "Score = 0";
                    }
                }
            }
        }
        
    }

    public void onPlusButtonPress(Button button, GameObject playerPrefab)
    {
        Debug.Log(button.GetComponentInChildren<TextMeshProUGUI>().text);
        string currentScoreText = playerPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        string[] currentScoreArray = currentScoreText.Split('=');
        Debug.Log(currentScoreArray[1]);
        int scoreVal = int.Parse(currentScoreArray[1]);
        scoreVal += 1;
        playerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Score = " + scoreVal.ToString();
    }

    public void onMinusButtonPress(Button button, GameObject playerPrefab)
    {
        Debug.Log(button.GetComponentInChildren<TextMeshProUGUI>().text);
        string currentScoreText = playerPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        string[] currentScoreArray = currentScoreText.Split('=');
        int scoreVal = int.Parse(currentScoreArray[1]);
        scoreVal -= 1;
        playerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Score = " + scoreVal.ToString();
    }

    public void onProfileButtonPressAsync()
    {
        SceneManager.LoadScene("ProfilePickerScene");
    }

    public void chatBack()
    {
        chatPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void topicPickDisplay()
    {
        gamePanel.SetActive(false);
        if (IsHost())
        {
            topicPickPanel.SetActive(true);
            setTopics();
        }
        StartCoroutine(pickTopicDelay());
    }

    public void setTopics()
    {
        Topic1.GetComponentInChildren<TextMeshProUGUI>().text = getRandomTopic(topics);
        Topic2.GetComponentInChildren<TextMeshProUGUI>().text = getRandomTopic(topics);
        Topic3.GetComponentInChildren<TextMeshProUGUI>().text = getRandomTopic(topics);
    }

    IEnumerator pickTopicDelay()
    {
        yield return new WaitForSeconds(5);
        if (currentTopic == "")
        {
            currentTopic = getRandomTopic(topics);
            topicText.text = currentTopic;
            chatIF.text = currentTopic;
            createChat();
        }
        topicPickPanel.SetActive(false);
        if (IsHost())
        {
            debateMasterPanel.SetActive(true);
            Debug.Log("opened master panel");
        }
        else
        {
            debateParticipantPanel.SetActive(true);
        }
        yield return new WaitForSeconds(10);
        debateMasterPanel.SetActive(false);
        debateParticipantPanel.SetActive(false);
        gamePanel.SetActive(true);
        timerObject.SetActive(true);
    }

    private string getRandomTopic(List<string> topics)
    {
        if(topics.Count < 3)
        {
            topics.Add("Minimum Wage");
            topics.Add("Mandatory Vaccinations");
            topics.Add("");
        }

        List<string> availableTopics = new List<string>(topics);
        availableTopics.RemoveAll(usedTopics.Contains);

        int randomIndex = Random.Range(0, availableTopics.Count);
        string selectedTopic = availableTopics[randomIndex];
        usedTopics.Add(selectedTopic);
        return selectedTopic;
    }

    public void onTopicBtn1Pressed()
    {
        currentTopic = Topic1.GetComponentInChildren<TextMeshProUGUI>().text;
        topicPickPanel.SetActive(false);
        topicText.text = currentTopic;
        chatIF.text = "the debate topic chosen is " +currentTopic;
        createChat();
    }

    public void onTopicBtn2Pressed()
    {
        currentTopic = Topic2.GetComponentInChildren<TextMeshProUGUI>().text;
        topicPickPanel.SetActive(false);
        topicText.text = currentTopic;
        chatIF.text = "the debate topic chosen is " + currentTopic;
        createChat();
    }

    public void onTopicBtn3Pressed()
    {
        currentTopic = Topic3.GetComponentInChildren<TextMeshProUGUI>().text;
        topicPickPanel.SetActive(false);
        topicText.text = currentTopic;
        chatIF.text = "the debate topic chosen is " + currentTopic;
        createChat();
    }

    List<string> getTopicsList()
    {
        string customTopicsListString = PlayerPrefs.GetString("CustomTopics");
        string[] customTopicsArray = customTopicsListString.Split(";");
        List<string> customTopicsList = new List<string>(customTopicsArray);
        Debug.Log(customTopicsList.Count);
        return customTopicsList;
    }

    public async void saveScores()
    {
        Debug.Log("button pressed for score");

        if (IsHost())
        {
            Debug.Log("is the host");
            List<GameObject> players = new List<GameObject>();
            Debug.Log(playerInfoContent.transform.childCount);
            for(int i = 0; i < playerContent.transform.childCount; i++)
            {
                players.Add(playerContent.transform.GetChild(i).gameObject);
                Debug.Log(playerContent.transform.childCount);
            }
            Debug.Log(players.Count);
            foreach(GameObject player in players)
            {
                TextMeshProUGUI[] texts = player.GetComponentsInChildren<TextMeshProUGUI>();
                string name = "";
                string score = "";
                foreach(TextMeshProUGUI text in texts)
                {
                    if (text.tag == "Name") 
                    {
                        name = text.text;
                        Debug.Log(name);
                    }
                    else if(text.tag == "Score")
                    {
                        score = text.text;
                        Debug.Log(score);
                    }
                }
                chatIF.text = name + " got the " + score;
                createChat();
            }
        }
    }

    void onInfoBtnPress()
    {
        gamePanel.SetActive(false);
        Debug.Log("info button pressed");
        // i need a way to like hide the game panel but the timer continues
        if (IsHost())
        {
            debateMasterInfoPanel.SetActive(true);
        }
        else
        {
            debateParticipantInfoPanel.SetActive(true);
        }
    }

    void onBackBtnDebateMasterInfo()
    {
        debateMasterInfoPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void onBackBtnDebateParticipantInfoPanel()
    {
        debateParticipantInfoPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public async void backToMainMenuAsync()
    {
        await UnityServices.InitializeAsync();
        await VivoxService.Instance.LogoutAsync();

        SceneManager.LoadScene("StartScreen");
    }

}

