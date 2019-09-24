using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAdjust : MonoBehaviour
{


    public PlayerControler playerMove;



    public int cameraWidthX = 5;
    public int cameraWidthY = 5;
    private int trueCameraWidth = 5;
    private Renderer mesh;
    private GameObject player;
    private Vector3 offset;
    public int facingSide = 1;
    private bool gameStart = false;
    public GameObject mainCamera;
    private GameObject fpsCamera;
    private GameObject miniMapCamera;
    private Vector3 cameraPos;





    //this will change the scale of the camera width
    public void Adjust()
    {
        fpsCamera = GameObject.Find("FPSCamera");
        miniMapCamera = GameObject.Find("MiniMapCamera");
        GameObject Maze = GameObject.Find("wallHolder");
        mesh = GetComponent<Renderer>();
        cameraPos = transform.position;

        if (!mazeGenerator.FPS)
        {
            //this will go if the Y value is the biggest
            if (mazeGenerator.staticYSize > mazeGenerator.staticXSize)
            {
                trueCameraWidth = cameraWidthY;

                if (!gameStart)
                {
                    transform.Rotate(0f, 0f, 90f);
                    mazeGenerator.cameraTurned = true;
                }
                //this will adjust the camera so that the camera is correct above the map
                if (cameraWidthY < 20)
                {
                    float cameraZoom = trueCameraWidth * Screen.height / Screen.width / 2f;
                    Camera.main.orthographicSize = cameraZoom;
                    //this will check if the Main camera is on
                    if (!mazeGenerator.FPS)
                        cameraPos.z += 0.5f;
                }
                //this will correctly adjust the camera on higher x values
                else
                {
                    float cameraZoom = trueCameraWidth * Screen.height / Screen.width / 2f + 0.5f;
                    Camera.main.orthographicSize = cameraZoom;
                }
            }
            //this will go if the X value is the biggest
            else
            {
                trueCameraWidth = cameraWidthX;
                //this will adjust the camera so that the camera is correct above the map
                if (cameraWidthX < 20)
                {
                    float cameraZoom = trueCameraWidth * Screen.height / Screen.width / 2f;
                    Camera.main.orthographicSize = cameraZoom;
                    //this will check if the Main camera is on
                    if (!mazeGenerator.FPS)
                        cameraPos.x += 0.5f;
                }
                //this will correctly adjust the camera on higher x values
                else
                {
                    float cameraZoom = trueCameraWidth * Screen.height / Screen.width / 2f + 0.5f;
                    Camera.main.orthographicSize = cameraZoom;
                }

            }

        }
        //this will check if the game has already started
        if (!gameStart)
        {
            gameStart = true;
            //this will activate the FPS modes and disable the Maincamera
            if (mazeGenerator.FPS)
            {
                mainCamera.SetActive(false);
                fpsCamera.SetActive(true);
                miniMapCamera.SetActive(true);
            }
            //this will disable the FPS modes and activate the Maincamera
            else
            {
                mainCamera.SetActive(true);
                fpsCamera.SetActive(false);
                miniMapCamera.SetActive(false);
            }
        }
    }
    //this will use the X slider to define the camera width 
    public void cameraUpdateX(float value)
    {
        cameraWidthX = Mathf.RoundToInt(value);
    }
    //this will use the Y slider to define the camera width 
    public void cameraUpdateY(float value)
    {
        cameraWidthY = Mathf.RoundToInt(value);
    }

    //this will deactivate the FPS camera and activate the Maincamera
    public void cameraWin()
    {
        //this will check if the FPS camera has been turned on
        if (mazeGenerator.FPS)
        {
            mainCamera.SetActive(true);
            fpsCamera.SetActive(false);
            miniMapCamera.SetActive(false);
            mazeGenerator.FPS = true;
        }
    }
}



//}
