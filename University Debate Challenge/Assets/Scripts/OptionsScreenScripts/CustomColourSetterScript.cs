using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomColourSetterScript : MonoBehaviour
{
    [SerializeField] private Button backBtn;
    [SerializeField] private Button setColourBtn;
    [SerializeField] private Button ResetBtn;

    [Space(10)]
    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown backgroundColourDropdown;
    [SerializeField] private TMP_Dropdown btnColourDropdown;
    [SerializeField] private TMP_Dropdown buttonTextColourDropdown;
    [SerializeField] private TMP_Dropdown textColourDropdown;

    [Space(10)]
    [Header("Colour Previews")]
    [SerializeField] private Canvas previewPanel;
    [SerializeField] private Button previewBtn;
    [SerializeField] private TextMeshProUGUI previewText;
    [SerializeField] private TMP_InputField previewInput;

    [Space(10)]
    [Header("Game Objects And Buttons")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private TMP_InputField[] inputs;

    private Color btnColor;
    private Color buttonTextColour;
    private Color backgroundColour;
    private Color textColour;

    private Color btndefColour = new Color(0, 0, 255 / 255f);
    private Color buttonTextdefColour = Color.white;
    private Color backgrounddefColour = new Color(0 / 255f, 205 / 255f, 255 / 255f);
    private Color textdefColour = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("TextColourR"))
        {

        }
        else
        {
            PlayerPrefs.SetFloat("BtnColourR", btndefColour.r);
            PlayerPrefs.SetFloat("BtnColourG", btndefColour.g);
            PlayerPrefs.SetFloat("BtnColourB", btndefColour.b);
            PlayerPrefs.SetFloat("ButtonTextColourR", buttonTextdefColour.r);
            PlayerPrefs.SetFloat("ButtonTextColourG", buttonTextdefColour.g);
            PlayerPrefs.SetFloat("ButtonTextColourB", buttonTextdefColour.b);
            PlayerPrefs.SetFloat("BgColourR", backgrounddefColour.r);
            PlayerPrefs.SetFloat("BgColourG", backgrounddefColour.g);
            PlayerPrefs.SetFloat("BgColourB", backgrounddefColour.b);
            PlayerPrefs.SetFloat("TextColourR", textdefColour.r);
            PlayerPrefs.SetFloat("TextColourG", textdefColour.g);
            PlayerPrefs.SetFloat("TextColourB", textdefColour.b);
        }


        backBtn.onClick.AddListener(backButtonPress);
        backgroundColourDropdown.onValueChanged.AddListener(onDropDownValChangeBg);
        btnColourDropdown.onValueChanged.AddListener(onDropDownValChangeBtn);
        textColourDropdown.onValueChanged.AddListener (onDropDownValChangeText);
        buttonTextColourDropdown.onValueChanged.AddListener(onDropDownValChangeButtonText);
        setColourBtn.onClick.AddListener(onColourSetPress);
        ResetBtn.onClick.AddListener(resetToDefaults);

        colourSettersButtons(buttons);
        colourSettersPanels(panels);
        colourSettersText(texts);
        colourSettersInputs(inputs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void backButtonPress()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    public void onDropDownValChangeBg(int index)
    {
        Debug.Log(backgroundColourDropdown.options[index].text);
        previewPanel.GetComponent<Image>().color = colourCompiler(backgroundColourDropdown.options[index].text);
        backgroundColour = colourCompiler(backgroundColourDropdown.options[index].text); 
    }

    public void onDropDownValChangeButtonText(int index)
    {
        Debug.Log(textColourDropdown.options[index].text);
        previewBtn.GetComponentInChildren<TextMeshProUGUI>().color = colourCompiler(buttonTextColourDropdown.options[index].text);
        TextMeshProUGUI[] inputs = previewInput.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI input in inputs)
        {
            input.color = colourCompiler(buttonTextColourDropdown.options[index].text);
        }
        buttonTextColour = colourCompiler(buttonTextColourDropdown.options[index].text);
    }

    public void onDropDownValChangeText(int index)
    {
        Debug.Log(textColourDropdown.options[index].text);
        previewText.color = colourCompiler(textColourDropdown.options[index].text);
        textColour = colourCompiler(textColourDropdown.options[index].text);
    }
    public void onDropDownValChangeBtn(int index)
    {
        Debug.Log(btnColourDropdown.options[index].text);
        previewBtn.GetComponent<Image>().color = colourCompiler(btnColourDropdown.options[index].text);
        previewInput.GetComponent<Image>().color = colourCompiler(btnColourDropdown.options[index].text);
        btnColor = colourCompiler(btnColourDropdown.options[index].text);
    }

    public Color colourCompiler(string colourString)
    {
        Color color = new Color();

        switch (colourString)
        {
            case "Red":
                color = new Color(255/255f, 0/255f, 0 / 255f);
                break;
            case "Yellow":
                color = new Color(249 / 255f, 246 / 255f, 43 / 255f);
                break;
            case "Pink":
                color = new Color(255 / 255f, 102 / 255f, 235 / 255f);
                break;
            case "Green":
                color = new Color(0 / 255f, 255 / 255f, 0 / 255f);
                break;
            case "Orange":
                color = new Color(255 / 255f, 101 / 255f, 38 / 255f);
                break;
            case "Purple":
                color = new Color(204 / 255f, 0 / 255f, 255 / 255f);
                break;
            case "Blue":
                color = new Color(0 / 255f, 0, 255 / 255f);
                break;
            case "Light Blue":
                color = new Color(0 / 255f, 205 / 255f, 255 / 255f);
                break;
            case "Black":
                color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
                break;
            case "White":
                color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
        }

        return color;
    }

    public void onColourSetPress()
    {
        PlayerPrefs.SetFloat("BtnColourR",btnColor.r);
        PlayerPrefs.SetFloat("BtnColourG", btnColor.g);
        PlayerPrefs.SetFloat("BtnColourB", btnColor.b);
        PlayerPrefs.SetFloat("ButtonTextColourR",buttonTextColour.r);
        PlayerPrefs.SetFloat("ButtonTextColourG", buttonTextColour.g);
        PlayerPrefs.SetFloat("ButtonTextColourB", buttonTextColour.b);
        PlayerPrefs.SetFloat("BgColourR",backgroundColour.r);
        PlayerPrefs.SetFloat("BgColourG", backgroundColour.g);
        PlayerPrefs.SetFloat("BgColourB", backgroundColour.b);
        PlayerPrefs.SetFloat("TextColourR", textColour.r);
        PlayerPrefs.SetFloat("TextColourG", textColour.g);
        PlayerPrefs.SetFloat("TextColourB", textColour.b);

        colourSettersButtons(buttons);
        colourSettersPanels(panels);
        colourSettersText(texts);
    }

    public void colourSettersButtons(Button[] buttons)
    {
       foreach(Button button in buttons)
        {
            button.GetComponent<Image>().color = new Color(
                PlayerPrefs.GetFloat("BtnColourR"),
                PlayerPrefs.GetFloat("BtnColourG"),
                PlayerPrefs.GetFloat("BtnColourB")
                );
            button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(
                PlayerPrefs.GetFloat("ButtonTextColourR"),
                PlayerPrefs.GetFloat("ButtonTextColourG"),
                PlayerPrefs.GetFloat("ButtonTextColourB")
                );
        }
    }

    public void colourSettersPanels(GameObject[] panels)
    {
        foreach(GameObject panel in panels)
        {
            panel.GetComponent<Image>().color = new Color(
                PlayerPrefs.GetFloat("BgColourR"),
                PlayerPrefs.GetFloat("BgColourG"),
                PlayerPrefs.GetFloat("BgColourB")
                ) ;
        }
    }

    public void colourSettersText(TextMeshProUGUI[] texts)
    {
        foreach(TextMeshProUGUI text in texts)
        {
            text.color = new Color(
                PlayerPrefs.GetFloat("TextColourR"),
                PlayerPrefs.GetFloat("TextColourG"),
                PlayerPrefs.GetFloat("TextColourB")
                );
        }
    }

    public void colourSettersInputs(TMP_InputField[] inputs)
    {
        foreach (TMP_InputField input in inputs)
        {
            input.GetComponent<Image>().color = new Color(
                PlayerPrefs.GetFloat("BtnColourR"),
                PlayerPrefs.GetFloat("BtnColourG"),
                PlayerPrefs.GetFloat("BtnColourB")
                );
            TextMeshProUGUI[] texts = input.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                text.color = new Color(
                PlayerPrefs.GetFloat("ButtonTextColourR"),
                PlayerPrefs.GetFloat("ButtonTextColourG"),
                PlayerPrefs.GetFloat("ButtonTextColourB")

                );
            }
        }
    }

    public void resetToDefaults()
    {
        PlayerPrefs.SetFloat("BtnColourR", btndefColour.r);
        PlayerPrefs.SetFloat("BtnColourG", btndefColour.g);
        PlayerPrefs.SetFloat("BtnColourB", btndefColour.b);
        PlayerPrefs.SetFloat("ButtonTextColourR", buttonTextdefColour.r);
        PlayerPrefs.SetFloat("ButtonTextColourG", buttonTextdefColour.g);
        PlayerPrefs.SetFloat("ButtonTextColourB", buttonTextdefColour.b);
        PlayerPrefs.SetFloat("BgColourR", backgrounddefColour.r);
        PlayerPrefs.SetFloat("BgColourG", backgrounddefColour.g);
        PlayerPrefs.SetFloat("BgColourB", backgrounddefColour.b);
        PlayerPrefs.SetFloat("TextColourR", textdefColour.r);
        PlayerPrefs.SetFloat("TextColourG", textdefColour.g);
        PlayerPrefs.SetFloat("TextColourB", textdefColour.b);

        colourSettersButtons(buttons);
        colourSettersPanels(panels);
        colourSettersText(texts);
        colourSettersInputs(inputs);
    } 
}
