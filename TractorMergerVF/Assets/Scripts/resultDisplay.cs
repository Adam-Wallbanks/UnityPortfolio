using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class resultDisplay : MonoBehaviour
{
    public GameObject obj;
    [SerializeField] private Button backToMainMenuBtn;

    // Start is called before the first frame update
    void Start()
    {
        WriteToFile writeToFile = new WriteToFile();
        List<Vector3> points1 = writeToFile.LoadVector3List("contour1");
        List<Vector3> points2 = writeToFile.LoadVector3List("contour2");
        List<Vector3> points3 = writeToFile.LoadVector3List("contour3");
        List<Vector3> points4 = writeToFile.LoadVector3List("contour4");
        List<Vector3> points5 = writeToFile.LoadVector3List("contour5");
        List<Vector3> points6 = writeToFile.LoadVector3List("contour6");
        List<Vector3> points7 = writeToFile.LoadVector3List("contour7");
        List<Vector3> points8 = writeToFile.LoadVector3List("contour8");
  
        List<Vector3> contour1Positions = contourCompiler(0, points1,points2,points3,points4,points5,points6,points7,points8);
        List<Vector3> contour2Positions = contourCompiler(1, points1, points2, points3, points4, points5, points6, points7, points8);
        List<Vector3> contour3Positions = contourCompiler(2, points1, points2, points3, points4, points5, points6, points7, points8);
        plotResults(contour1Positions,contour2Positions,contour3Positions);
        drawLines(contour1Positions, Color.red,0);
        drawLines(contour2Positions, Color.blue,1);
        drawLines(contour3Positions, Color.green,2);

        backToMainMenuBtn.onClick.AddListener(backToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void plotResults(List<Vector3> con1 ,List<Vector3> con2 ,List<Vector3> con3)
    {
        for (int i = 0; i < con1.Count; i++) {
            GameObject con1dot = Instantiate(obj, con1[i], Quaternion.identity);
            Renderer renderer= con1dot.GetComponent<Renderer>();
            Material mat = renderer.material;
            mat.color = Color.red;
            con1dot.tag = "target" + (i + 1) + "0";
        }

        for (int i = 0; i < con2.Count; i++)
        {
            GameObject con2dot = Instantiate(obj, con2[i], Quaternion.identity);
            Renderer renderer = con2dot.GetComponent<Renderer>();
            Material mat = renderer.material;
            mat.color = Color.blue;
            con2dot.tag= "target" + (i+1)+"1";
        }

        for (int i = 0; i < con3.Count; i++)
        {
            GameObject con3dot = Instantiate(obj, con3[i], Quaternion.identity);
            Renderer renderer = con3dot.GetComponent<Renderer>();
            Material mat = renderer.material;
            mat.color = Color.green;
            con3dot.tag = "target" + (i + 1)+"2";
        }

    }

    List<Vector3> contourCompiler(int num, List<Vector3> con1, List<Vector3> con2, List<Vector3> con3, List<Vector3> con4, List<Vector3> con5, List<Vector3> con6, List<Vector3> con7, List<Vector3> con8)
    {
        List<Vector3> returnVector= new List<Vector3>();
        switch (num)
        {
            case 0:
                returnVector.Add(con1[num]);
                returnVector.Add(con2[num]);
                returnVector.Add(con3[num]);
                returnVector.Add(con4[num]);
                returnVector.Add(con5[num]);
                returnVector.Add(con6[num]);
                returnVector.Add(con7[num]);
                returnVector.Add(con8[num]);
                break;
            case 1:
                returnVector.Add(con1[num]);
                returnVector.Add(con2[num]);
                returnVector.Add(con3[num]);
                returnVector.Add(con4[num]);
                returnVector.Add(con5[num]);
                returnVector.Add(con6[num]);
                returnVector.Add(con7[num]);
                returnVector.Add(con8[num]);
                break;
            case 2:
                returnVector.Add(con1[num]);
                returnVector.Add(con2[num]);
                returnVector.Add(con3[num]);
                returnVector.Add(con4[num]);
                returnVector.Add(con5[num]);
                returnVector.Add(con6[num]);
                returnVector.Add(con7[num]);
                returnVector.Add(con8[num]);
                break;
        }
        return returnVector;
    }

    void drawLines(List<Vector3> listVec, Color color, int tagNum)
    {
        string tagString = tagNum.ToString();
        Material mat = Resources.Load<Material>("Materials/red");
        
        if(color == Color.red)
        {
            mat = Resources.Load<Material>("Materials/red");
        }
        if(color == Color.blue)
        {
            mat = Resources.Load<Material>("Materials/blue");
        }
        if(color == Color.green)
        {
            mat = Resources.Load<Material>("Materials/green");
        }

        for (int i = 0; i < listVec.Count - 1; i++)
        {
          
                GameObject target1 = GameObject.FindGameObjectWithTag("target" +(i+1)+tagString);
                GameObject target2 = GameObject.FindGameObjectWithTag("target" +(i+2)+ tagString);
                if (target1 != null)
                {
                    LineRenderer lineRenderer = target1.GetComponent<LineRenderer>();
                    if (lineRenderer == null)
                    {
                        lineRenderer = target1.AddComponent<LineRenderer>();
                        lineRenderer.startWidth = 0.1f;
                        lineRenderer.endWidth = 0.1f;
                        lineRenderer.material = mat;
                    }
                    lineRenderer.SetPosition(0, target1.transform.position);
                    lineRenderer.SetPosition(1, target2.transform.position);
                }
            
            if (i == listVec.Count - 2)
            {
                LineRenderer lineRenderer2 = target2.AddComponent<LineRenderer>();
                lineRenderer2.startWidth = 0.1f;
                lineRenderer2.endWidth = 0.1f;
                lineRenderer2.material = mat;
                lineRenderer2.SetPosition(0, target2.transform.position);
                lineRenderer2.SetPosition(1, GameObject.FindGameObjectWithTag("target"+"1"+tagString).transform.position);
            }
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
