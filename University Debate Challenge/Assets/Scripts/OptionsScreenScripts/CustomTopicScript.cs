using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomTopicScript : MonoBehaviour
{
    [Header("Topics List")]
    [SerializeField] private GameObject topicInputPrefab;
    [SerializeField] private GameObject topicListContent;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button PlusBtn;
    [SerializeField] private Button setTopicsBtn;
    [SerializeField] private Button MinusBtn;

    public List<string> customTopics;
    private List<GameObject> topicObjs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(backButtonPress);
        PlusBtn.onClick.AddListener(onAddBtnPress);
        setTopicsBtn.onClick.AddListener(setTopicsList);
        MinusBtn.onClick.AddListener(onMinBtnPress);

        if (PlayerPrefs.HasKey("CustomTopics")) 
        {
            getTopicsList();        
        }
        for(int i = 0; i < customTopics.Count; i++)
        {
            GameObject topicInput = Instantiate(topicInputPrefab, topicListContent.transform);
            topicObjs.Add(topicInput);
            TMP_InputField tmpInputFieldComponent = topicInput.GetComponentInChildren<TMP_InputField>();
            TextMeshProUGUI[] texts = topicInput.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(texts.Length);
            texts[0].text = i.ToString();
            if (tmpInputFieldComponent != null)
            {
                tmpInputFieldComponent.text = customTopics[i];
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onAddBtnPress()
    {
        GameObject textInput = Instantiate(topicInputPrefab, topicListContent.transform);
        int length = topicObjs.Count;
        if(length > 0)
        {
            TextMeshProUGUI[] texts = textInput.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(texts.Length);
            texts[0].text = (topicObjs.Count).ToString();
        }
        else
        {
            TextMeshProUGUI[] texts = textInput.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(texts.Length);
            texts[0].text = "0";
        }
        topicObjs.Add(textInput);
        setTopicsList();
        Debug.Log("add button press");
    }

    void backButtonPress()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    void setTopicsList()
    {
        TMP_InputField[] inputFields = topicListContent.GetComponentsInChildren<TMP_InputField>();
        foreach(TMP_InputField inputField in inputFields)
        {
            customTopics.Add(inputField.text);
        }
        customTopics = RemoveDuplicates(customTopics);
        string joinedList = string.Join(";", customTopics.ToArray());
        PlayerPrefs.SetString("CustomTopics",joinedList);
    }

    void getTopicsList()
    {
        string customTopicsListString = PlayerPrefs.GetString("CustomTopics");
        string[] customTopicsArray = customTopicsListString.Split(";");
        List<string> customTopicsList = new List<string>(customTopicsArray);
        customTopics = customTopicsList;
        Debug.Log(customTopicsList.Count);
    }

    public List<string> RemoveDuplicates(List<string> originalList)
    {
        List<string> uniqueList = new List<string>();

        foreach (string item in originalList)
        {
            if (!uniqueList.Contains(item))
            {
                uniqueList.Add(item);
            }
        }

        return uniqueList;
    }

    public void onMinBtnPress()
    {
        GameObject bottomTopic = topicObjs[topicObjs.Count - 1];
        topicObjs.RemoveAt(topicObjs.Count - 1);
        string topicToRemove = bottomTopic.GetComponentInChildren<TMP_InputField>().text;
        customTopics.Remove(topicToRemove);
        Destroy(bottomTopic);
    }
}
