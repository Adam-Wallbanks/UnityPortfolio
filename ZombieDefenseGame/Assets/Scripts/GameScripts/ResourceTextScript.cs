using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTextScript : MonoBehaviour
{
    public MinerScript playerResources;
    public TextMeshProUGUI resourceText;

    // Start is called before the first frame update
    void Start()
    {
        resourceText.text = "\U0001F5FF : " + playerResources.totalResources;

    }

    // Update is called once per frame
    void Update()
    {
        resourceText.text = "\U0001F5FF : " + playerResources.totalResources;

    }
}
