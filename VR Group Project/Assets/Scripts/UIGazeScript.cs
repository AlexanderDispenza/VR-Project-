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
    public float delay = 2.0f;
    float timer;
    float scaleFactor;

    Transform RightHand;
    Transform LeftHand;

    public Image ProgressBar;
    public TextMeshProUGUI TimerText;

    public SteamVR_ActionSet m_ActionSet;
    public SteamVR_Action_Boolean m_BooleanAction;
    public SteamVR_Action_Vector2 m_TouchPosition;

    void Awake()
    {
        RightHand = GameObject.Find("Player/SteamVRObjects/RightHand").transform;
        LeftHand = GameObject.Find("Player/SteamVRObjects/LeftHand").transform;
        m_BooleanAction = SteamVR_Actions._default.GrabPinch; 
    }

    void Start()
    {
        button = GetComponent<Button>();
        m_ActionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }

    void Update()
    {
        isSelected = false;

        scaleFactor = timer / delay; 

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
                TimerText.text = timer.ToString();
                ProgressBar.fillAmount = Mathf.Lerp(0, 1, scaleFactor);

                if (timer >= delay && m_BooleanAction.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    button.onClick.Invoke();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Debug.Log("Button Pressed!");
                }
            }
        }
        else
        {
            if (EventSystem.current)
                EventSystem.current.SetSelectedGameObject(null);
            timer = 0;
            ProgressBar.fillAmount = 0.0f; 
        }
    }
}