using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    public Button helpBtn;

    // Start is called before the first frame update
    void Start()
    {
        helpBtn.onClick.AddListener(onHelpBtnPress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onHelpBtnPress() {
        Application.OpenURL("https://www.gov.uk/government/news/guidance-on-promoting-british-values-in-schools-published");
    }
}
