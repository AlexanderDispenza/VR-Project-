using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Valve.VR;
using UnityEngine.SceneManagement;
using System.Collections;

public class LocationGazeScript : MonoBehaviour
{
    public bool isSelected;
    public bool timedClick = true;
    public bool clicker = true;
    public bool highlight = true;

    public float delay;
    float timer;
    float scaleFactor;
    float maximumFOV;
    float minFOV; 

    Transform camera;

    public Image ProgressBar;

    public GameObject MainPanel;
    public GameObject Exit;

    AudioSource ReadInfo; 

    void Awake()
    {
        camera = Camera.main.transform;
        MainPanel = GameObject.Find("Loc/Canvas/Panel");
        Exit = GameObject.Find("Loc/DoorExit/Canvas/Panel");
        ProgressBar = GameObject.Find("Loc/DoorExit/Canvas/Panel/ProgressBar").GetComponent<Image>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        delay = 3.0f;
        maximumFOV = 120.0f;
        minFOV = -65.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        isSelected = false;

        Ray CameraRayCast = new Ray(camera.position, camera.rotation * Vector3.forward);

        RaycastHit hit;

        Debug.DrawRay(camera.position, camera.rotation * Vector3.forward * 200, Color.green);

        if (Physics.Raycast(CameraRayCast, out hit))
        {
            isSelected = true;

            if (hit.collider.tag == "ShowUI")
            {
                MainPanel.SetActive(true);
            }

            else if (hit.collider.tag == "Exit" && timedClick)
            {
                Exit.SetActive(true);

                timer += Time.deltaTime;
                scaleFactor = timer / delay;
                ProgressBar.fillAmount = scaleFactor;

                if (timer >= delay)
                {
                    StartCoroutine(ExitFOV());
                }
            }
        } 

        else
        {
            MainPanel.SetActive(false);
            Exit.SetActive(false);
            timer = 0.0f;
            ProgressBar.fillAmount = 0.0f; 
        }
    }

    IEnumerator ExitFOV()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, maximumFOV, 0.05f);
        yield return new WaitForSeconds(0.4f);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, minFOV, 0.05f);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
