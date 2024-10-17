using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIColourManager : MonoBehaviour
{
    [Space(10)]
    [Header("UI Elements")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private TMP_InputField[] inputFields;
    [SerializeField] private Text toggleLabel;

    private Color btnColour = new Color(0,0,255/255f);
    private Color buttonTextColour = Color.white;
    private Color backgroundColour = new Color(0 / 255f, 205 / 255f, 255 / 255f);
    private Color textColour = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("TextColourR"))
        {

        }
        else 
        {
            PlayerPrefs.SetFloat("BtnColourR", btnColour.r);
            PlayerPrefs.SetFloat("BtnColourG", btnColour.g);
            PlayerPrefs.SetFloat("BtnColourB", btnColour.b);
            PlayerPrefs.SetFloat("ButtonTextColourR", buttonTextColour.r);
            PlayerPrefs.SetFloat("ButtonTextColourG", buttonTextColour.g);
            PlayerPrefs.SetFloat("ButtonTextColourB", buttonTextColour.b);
            PlayerPrefs.SetFloat("BgColourR", backgroundColour.r);
            PlayerPrefs.SetFloat("BgColourG", backgroundColour.g);
            PlayerPrefs.SetFloat("BgColourB", backgroundColour.b);
            PlayerPrefs.SetFloat("TextColourR", textColour.r);
            PlayerPrefs.SetFloat("TextColourG", textColour.g);
            PlayerPrefs.SetFloat("TextColourB", textColour.b);
        }


        CustomColourSetterScript customColourSetterScript = new CustomColourSetterScript();
        customColourSetterScript.colourSettersPanels(panels);
        customColourSetterScript.colourSettersButtons(buttons);
        customColourSetterScript.colourSettersText(texts);
        customColourSetterScript.colourSettersInputs(inputFields);

        if (toggleLabel != null)
        {
            toggleLabel.color = new Color(
                    PlayerPrefs.GetFloat("TextColourR"),
                    PlayerPrefs.GetFloat("TextColourG"),
                    PlayerPrefs.GetFloat("TextColourB")
                    );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
