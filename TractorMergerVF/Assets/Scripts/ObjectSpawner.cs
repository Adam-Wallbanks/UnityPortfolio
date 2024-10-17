using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject sceneChangeBtn;
    public AudioSource clickSoundClip;
    public AudioSource endSoundClip;
    public AudioSource startSoundClip;
    public bool dressGame;
    public bool tractorGame;
    public GameObject[] spawners;
    public ParticleSystem smokeSystem;
    public List<Sprite> tractors;
    private List<GameObject> spawnedObjs = new List<GameObject>();
    private SpriteRenderer dressRenderer;
    private SpriteRenderer tractorRenderer;
    private int dressNum = 0;
    private int tractorNum = 0;
    public GameObject toSpawn;
    public float speed = 3.0f;
    int spawn1 = 0;
    int spawn2 = 0;
    int spawn3 = 0;
    int spawn4 = 0;
    int spawn5 = 0;
    int spawn6 = 0;
    int spawn7 = 0;
    int spawn8 = 0;
    int spawn9 = 0;
    int spawn10 = 0;
    int spawn11 = 0;
    int spawn12 = 0;
    bool spawn1Bool = true;
    bool spawn2Bool = true;
    bool spawn3Bool = true;
    bool spawn4Bool = true;
    bool spawn5Bool = true;
    bool spawn6Bool = true;
    bool spawn7Bool = true;
    bool spawn8Bool = true;
    bool spawn9Bool = true;
    bool spawn10Bool = true;
    bool spawn11Bool = true;
    bool spawn12Bool = true;
    public float contourVal = 0f;
    public float contourDiv;
    int clickedCount = 0;
    public static List<Vector3> spawn1Pos = new List<Vector3>();
    public static List<Vector3> spawn2Pos = new List<Vector3>();
    public static List<Vector3> spawn3Pos = new List<Vector3>();
    public static List<Vector3> spawn4Pos = new List<Vector3>();
    public static List<Vector3> spawn5Pos = new List<Vector3>();
    public static List<Vector3> spawn6Pos = new List<Vector3>();
    public static List<Vector3> spawn7Pos = new List<Vector3>();
    public static List<Vector3> spawn8Pos = new List<Vector3>();
    public static List<Vector3> spawn9Pos = new List<Vector3>();
    public static List<Vector3> spawn10Pos = new List<Vector3>();
    public static List<Vector3> spawn11Pos = new List<Vector3>();
    public static List<Vector3> spawn12Pos = new List<Vector3>();
    [SerializeField] private Button nextButton;

    // Start is called before the first frame update
    void Start()
    {
        startSoundClip.volume = PlayerPrefs.GetFloat("VolumeVal");
        startSoundClip.Play();
        spawnTargets();
        contourDiv = PlayerPrefs.GetFloat("ContourDivFloat");
        if(contourDiv == 0)
        {
            contourDiv = 2;
        }
        if (PlayerPrefs.HasKey("VolumeVal"))
        {

        }
        else
        {
            PlayerPrefs.SetFloat("VolumeVal", 1);
        }

        nextButton.onClick.AddListener(onNextBtnPress);
        nextButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spawner in spawnedObjs)
        {            
            switch (spawner.tag)
            {
                case "sp1":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(7.26000023f, -11f, 0f), speed * Time.deltaTime);
                    break;
                case "sp2":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(-7.26000023f, -11, 0), speed * Time.deltaTime);
                    break;
                case "sp3":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(-20, -9, 0), speed * Time.deltaTime);
                    break;
                case "sp4":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(-20, 8, 0), speed * Time.deltaTime);
                    break;
                case "sp5":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(-7.26000023f, 11, 0), speed * Time.deltaTime);
                    break;
                case "sp6":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(7.26000023f, 11, 0), speed * Time.deltaTime);
                    break;
                case "sp7":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(20, 8, 0), speed * Time.deltaTime);
                    break;
                case "sp8":
                    spawner.transform.position = Vector3.MoveTowards(spawner.transform.position, new Vector3(20, -9, 0), speed * Time.deltaTime);
                    break;

            }
            
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Vector3 objPos = new Vector3();
                    string tagName = hit.collider.tag;
                    if (hit.collider.transform != null)
                    {
                        objPos = hit.collider.transform.position;
                        Renderer objRenderer = hit.collider.GetComponent<Renderer>();
                        Material currentMaterial= objRenderer.material;
                        Color currentColour = currentMaterial.color;
                        float currentContour = currentColour.a;
                        if (dressGame) {
                            dressChange(tractors);
                        }
                        if (tractorGame)
                        {
                            tractorChange(tractors);
                        }
                    }
                    switch(tagName){
                        case "sp1":
                            spawn1Pos.Add(objPos);
                            spawners[0].transform.position = objPos;
                            spawn1Bool = false;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = true;
                            spawn1 += 1;
                            break;
                        case "sp2":
                            spawn2Pos.Add(objPos);
                            spawners[1].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = false;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = true; 
                            spawn2 += 1;
                            break;
                        case "sp3":
                            spawn3Pos.Add(objPos);
                            spawners[2].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = false;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = true;
                            spawn3 += 1;
                            break;
                        case "sp4":
                            spawn4Pos.Add(objPos);
                            spawners[3].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = false;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = true;
                            spawn4 += 1;
                            break;
                        case "sp5":
                            spawn5Pos.Add(objPos);
                            spawners[4].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = false;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = true;
                            spawn5 += 1;
                            break;
                        case "sp6":
                            spawn6Pos.Add(objPos);
                            spawners[5].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = false;
                            spawn7Bool = true;
                            spawn8Bool = true;
                            spawn6 += 1;
                            break;
                        case "sp7":
                            spawn7Pos.Add(objPos);
                            spawners[6].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = false;
                            spawn8Bool = true;
                            spawn7 += 1;
                            break;
                        case "sp8":
                            spawn8Pos.Add(objPos);
                            spawners[7].transform.position = objPos;
                            spawn1Bool = true;
                            spawn2Bool = true;
                            spawn3Bool = true;
                            spawn4Bool = true;
                            spawn5Bool = true;
                            spawn6Bool = true;
                            spawn7Bool = true;
                            spawn8Bool = false;
                            spawn8 += 1;
                            break;

                    }
                    clickedCount += 1;
                    DestroyAllGameObjects();
                    spawnedObjs.Clear();
                    StartCoroutine(spawnDelay());
                    nextButton.gameObject.SetActive(true);
                    
                }
            }

        }

        if(clickedCount >= 24)
        {
            endSoundClip.volume = PlayerPrefs.GetFloat("VolumeVal");
            endSoundClip.Play();

            nextButton.gameObject.SetActive(false);

            WriteToFile writeToFile = new WriteToFile();
            writeToFile.FileWriting(spawn1Pos,spawn2Pos,spawn3Pos,spawn4Pos,spawn5Pos,spawn6Pos,spawn7Pos,spawn8Pos);

            Debug.Log(spawn1Pos.Count);

            sceneChangeBtn.transform.position = new Vector3(0, 0, 0);
        }

        if(clickedCount == 23)
        {
            spawn1Bool = true;
            spawn2Bool = true;
            spawn3Bool = true;
            spawn4Bool = true;
            spawn5Bool = true;
            spawn6Bool = true;
            spawn7Bool = true;
            spawn8Bool = true;
        }
    }

    void DestroyAllGameObjects()
    {
        foreach (GameObject obj in spawnedObjs) { 
            Destroy(obj);
        }
    }

    void spawnTargets()
    {
        bool canSpawn = false;
        foreach (GameObject spawner in spawners)
        {
            string spawnTag = spawner.tag;
            string tagEnd = spawnTag.Substring(2);
            Debug.Log(tagEnd);
            switch (tagEnd)
            {
                case "1":
                    switch (spawn1)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn1Bool;
                            break;
                        case 1:
                            contourVal = 1.0f/contourDiv;
                            canSpawn = spawn1Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn1Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break; 
                case "2":
                    switch (spawn2)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn2Bool;
                            break;
                        case 1:
                            contourVal = 1.0f / contourDiv;
                            canSpawn = spawn2Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn2Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "3":
                    switch (spawn3)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn3Bool;
                            break;
                        case 1:
                            contourVal = 1.0f / contourDiv;
                            canSpawn = spawn3Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn3Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "4":
                    switch (spawn4)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn4Bool;
                            break;
                        case 1:
                            contourVal = 1.0f / contourDiv;
                            canSpawn = spawn4Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn4Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "5":
                    switch (spawn5)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn5Bool;
                            break;
                        case 1:
                            contourVal = 1.0f/contourDiv;
                            canSpawn = spawn5Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn5Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "6":
                    switch (spawn6)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn6Bool;
                            break;
                        case 1:
                            contourVal = 1.0f / contourDiv;
                            canSpawn = spawn6Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn6Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "7":
                    switch (spawn7)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn7Bool;
                            break;
                        case 1:
                            contourVal = 1.0f / contourDiv;
                            canSpawn = spawn7Bool;
                            break;
                        case 2:
                            contourVal = 1.0f / Mathf.Pow(contourDiv, 2);
                            canSpawn = spawn7Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
                case "8":
                    switch (spawn8)
                    {
                        case 0:
                            contourVal = 1.0f;
                            canSpawn = spawn8Bool;
                            break;
                        case 1:
                            contourVal = 1.0f/contourDiv;
                            canSpawn = spawn8Bool;
                            break;
                        case 2:
                            contourVal = 1.0f/Mathf.Pow(contourDiv,2);
                            canSpawn = spawn8Bool;
                            break;
                        case >= 3:
                            contourVal = 0.0f;
                            break;
                    }
                    break;
            }
            if (contourVal != 0.0f && canSpawn)
            {
                GameObject newObj = Instantiate(toSpawn, spawner.transform.position, Quaternion.identity);
                Renderer objRenderer = newObj.GetComponent<Renderer>();
                Material currentMat = objRenderer.material;
                Color colour = currentMat.color;
                colour.a = contourVal;
                currentMat.color = colour;
                newObj.tag = spawnTag;
                spawnedObjs.Add(newObj);
            }
        }
    }

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(0.4f);
    }

    IEnumerator changeSceneDelay()
    {
        yield return new WaitForSeconds(3);
    }

    public void dressChange(List<Sprite> dresses)
    {
        dressRenderer = GameObject.FindGameObjectWithTag("dress").GetComponent<SpriteRenderer>();
        if (dressNum == dresses.Count)
        {
            dressNum = 0;
        }
        else {
            dressRenderer.sprite = dresses[dressNum];
            dressNum++;
        }
    }

    public void tractorChange(List<Sprite> tractors)
    {
        tractorRenderer = GameObject.FindGameObjectWithTag("tractor").GetComponent<SpriteRenderer>();
        Debug.Log(tractorNum);
        if (tractorNum == tractors.Count)
        {
            tractorNum = 0;
        }
        else
        {
            tractorRenderer.sprite = tractors[tractorNum];
            tractorNum++;
        }
    }


    public void onNextBtnPress()
    {
        smokeSystem.Play();
        clickSoundClip.volume = PlayerPrefs.GetFloat("VolumeVal");
        clickSoundClip.Play();
        spawnTargets();
        nextButton.gameObject.SetActive(false);
    }
}
