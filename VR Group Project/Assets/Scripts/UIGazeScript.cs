using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Valve.VR;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIGazeScript: MonoBehaviour
{
    Button button;
    Button GazeBasedIndicatorButton;

    public bool isSelected;
    public bool timedClick = true;
    public bool clicker = true;
    public bool highlight = true;
    public bool isGazeBazed = true;
    bool SelectAudioPlayed = false; 

    public string inputButton = "Fire1";


    public float delay; 
    float timer;
    float scaleFactor;
    float volume; 

    Transform RightHand;
    Transform LeftHand;
    Transform camera; 

    public Image ProgressBar;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI VolumeText; 
    public TextMeshProUGUI GazeBasedIndicatorText;

    public GameObject MainPanel;
    public GameObject OptionsPanel;

    AudioSource Intro;
    AudioSource Click;
    AudioSource Select; 

    void Awake()
    {
        RightHand = GameObject.Find("Player/SteamVRObjects/RightHand").transform;
        LeftHand = GameObject.Find("Player/SteamVRObjects/LeftHand").transform;
        camera = Camera.main.transform; 
        MainPanel = GameObject.Find("Menu/Canvas/Main/MainPanel");
        OptionsPanel = GameObject.Find("Menu/Canvas/Main/Settings");
        GazeBasedIndicatorButton = GameObject.Find("Menu/Canvas/Main/Settings/GBIndicator").GetComponent<Button>();
        GazeBasedIndicatorText = GameObject.Find("Menu/Canvas/Main/Settings/GBIndicator/Text").GetComponent<TextMeshProUGUI>();
        VolumeText = GameObject.Find("Menu/Canvas/Main/Settings/VolumeAmount").GetComponent<TextMeshProUGUI>();
        Intro = GameObject.Find("AudioClips/Intro").GetComponent<AudioSource>();
        Click = GameObject.Find("AudioClips/Click").GetComponent<AudioSource>();
        Select = GameObject.Find("AudioClips/Select").GetComponent<AudioSource>();
    }

    void Start()
    {
        button = GetComponent<Button>();
        delay = 3.0f;
        volume = 1.0f;
        Intro.Play(); 
    }

    void Update()
    {
        isSelected = false;

        Ray RightHandRayCast = new Ray(RightHand.position, RightHand.rotation * Vector3.forward);
        Ray LeftHandRayCast = new Ray(LeftHand.position, LeftHand.rotation * Vector3.forward);
        Ray CameraRayCast = new Ray(camera.position, camera.rotation * Vector3.forward);

        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hit3; 

        Debug.DrawRay(RightHand.position, RightHand.rotation * Vector3.forward * 100, Color.red);
        Debug.DrawRay(LeftHand.position, LeftHand.rotation * Vector3.forward * 100, Color.red);
        Debug.DrawRay(camera.position, camera.rotation * Vector3.forward * 200, Color.green);


        if (Physics.Raycast(RightHandRayCast, out hit))
        {
            isSelected = true;
            if (timedClick && isGazeBazed == false)
            {
                timer += Time.deltaTime;
                scaleFactor = timer / delay;
                TimerText.text = timer.ToString();
                ProgressBar.fillAmount = scaleFactor;

                button = hit.collider.transform.parent.GetComponent<Button>();
                button.Select();

                if (SelectAudioPlayed == false)
                {
                    Select.Play();
                    SelectAudioPlayed = true; 
                }           
            }
        }

        else if (Physics.Raycast(LeftHandRayCast, out hit2))
        {
            isSelected = true;

            if (timedClick && isGazeBazed == false)
            {
                timer += Time.deltaTime;
                scaleFactor = timer / delay;
                TimerText.text = timer.ToString();
                ProgressBar.fillAmount = scaleFactor;

                button = hit2.collider.transform.parent.GetComponent<Button>();
                button.Select();

                if (SelectAudioPlayed == false)
                {
                    Select.Play();
                    SelectAudioPlayed = true;
                }
            }
        }

        else if (Physics.Raycast(CameraRayCast, out hit3))
        {

            isSelected = true;

            if (timedClick && isGazeBazed == true)
            {
                timer += Time.deltaTime;
                scaleFactor = timer / delay;
                TimerText.text = timer.ToString();
                ProgressBar.fillAmount = scaleFactor;

                button = hit3.collider.transform.parent.GetComponent<Button>();
                button.Select();

                if (SelectAudioPlayed == false)
                {
                    Select.Play();
                    SelectAudioPlayed = true;
                }

                if (timer >= delay && hit3.collider != null && hit3.collider.transform.parent.tag.Equals("UIButton") && isGazeBazed == true)
                {
                    button.onClick.Invoke();
                }
            }
        }
    }




    public void StartGame()
    {
        if (timer >= delay)
        {
            Click.Play();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Button Pressed!");
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void OpenOptions()
    {
        if (timer >= delay)
        {
            Click.Play();

            MainPanel.SetActive(false);
            OptionsPanel.SetActive(true);
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void CloseOptions()
    {
        if (timer >= delay)
        {
            Click.Play();

            OptionsPanel.SetActive(false);
            MainPanel.SetActive(true);
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void ExitGame()
    {
        if (timer >= delay)
        {
            Click.Play();

            Application.Quit();
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void SwitchGazeBazedInteraction()
    {
        if (timer >= delay)
        {
            Click.Play();

            var GetColors = GazeBasedIndicatorButton.colors; 
            isGazeBazed = !isGazeBazed;
            GazeBasedIndicatorText.text = isGazeBazed.ToString();

            if (GazeBasedIndicatorText.text == "False")
            {
              
            }


            else
            {
                
            }

            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void IncreaseVolume()
    {
        if (timer >= delay)
        {
            Click.Play();

            Intro.volume = volume;
            Click.volume = volume;
            Select.volume = volume; 

            volume = volume + 0.5f;
            VolumeText.text = "(" + volume.ToString() + ")";
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }

    public void DecreaseVolume()
    {
        if (timer >= delay)
        {
            Click.Play();

            Intro.volume = volume;
            Click.volume = volume;
            Select.volume = volume;

            volume = volume - 0.5f;
            VolumeText.text = "(" + volume.ToString() + ")";
            timer = 0.0f;
            TimerText.text = "0.00";
            ProgressBar.fillAmount = 0.0f;
            SelectAudioPlayed = false;
        }
    }
}