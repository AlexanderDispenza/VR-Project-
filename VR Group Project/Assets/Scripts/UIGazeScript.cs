using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Valve.VR;
using UnityEngine.SceneManagement;

public class UIGazeScript: MonoBehaviour
{
    public bool highlight = true;
    Button button;
    bool isSelected;

    public bool clicker = true;
    public string inputButton = "Fire1";

    public bool timedClick = true;
    public float delay; 
    float timer;
    float scaleFactor;

    Transform RightHand;
    Transform LeftHand;

    public Image ProgressBar;
    public TextMeshProUGUI TimerText;



    void Awake()
    {
        RightHand = GameObject.Find("Player/SteamVRObjects/RightHand").transform;
        LeftHand = GameObject.Find("Player/SteamVRObjects/LeftHand").transform;
    }

    void Start()
    {
        button = GetComponent<Button>();
        delay = 3.0f; 
    }

    void Update()
    {
        isSelected = false;

        Ray RightHandRayCast = new Ray(RightHand.position, RightHand.rotation * Vector3.forward);
        Ray LeftHandRayCast = new Ray(LeftHand.position, LeftHand.rotation * Vector3.forward);

        RaycastHit hit;
        RaycastHit hit2; 

        Debug.DrawRay(RightHand.position, RightHand.rotation * Vector3.forward * 100, Color.red);
        Debug.DrawRay(LeftHand.position, LeftHand.rotation * Vector3.forward * 100, Color.red);

        if (Physics.Raycast(RightHandRayCast, out hit) && hit.transform.parent &&
            (hit.transform.parent.gameObject == gameObject))
        {
            isSelected = true;
        }

        if (Physics.Raycast(LeftHandRayCast, out hit2) && hit2.transform.parent &&
            (hit2.transform.parent.gameObject == gameObject))
        {
            isSelected = true; 
        }

            if (isSelected)
        {
            if (highlight)
                button.Select();

            if (clicker && Input.GetButtonDown(inputButton))
                button.onClick.Invoke();

            if (timedClick)
            {
                timer += Time.deltaTime;
                scaleFactor = timer / delay;
                TimerText.text = timer.ToString();
                ProgressBar.fillAmount = scaleFactor;

                if (timer >= delay + 5.0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

        else
        {
            if (EventSystem.current)
                EventSystem.current.SetSelectedGameObject(null);
            timer = 0;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f; 
        }
    }


    public void StartGame ()
    {
        if (timer >= delay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Button Pressed!");
        }
    }
}