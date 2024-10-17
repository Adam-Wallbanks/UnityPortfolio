using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayContentWarningScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playContentWarning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playContentWarning() {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("StartScreen");
    }
}
