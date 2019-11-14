using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    //1. 
    [Header ("Level Props")]
    public GameObject crate;
    public GameObject trafficCone;
    public GameObject board;
    public GameObject wheel;
    public GameObject oilDrum;

    //2.
    [Header("Tank movment")]
    public int playerNumber = 1;

    //3.
    [Range(5f, 50f)]
    public int speed = 15;

    //4.
    [Range(1f, 90f)]
    public int turnSpeed = 120;

    //5.
    [Range(5f, 50f)]
    public float velocity = 20f;

    //6.

    //7.
    [Header("Tank Shoot")]
    public GameObject shell1;
    public Transform shootingPosition;

    //8.
    public float shootForce = 1000;

    public float minLaunchForce = 5f;
    public float maxLaunchForce = 25f;
    public float maxChargeTime = 1.5f;

    //9.
    //10.
    public float currentHealth = 100f;

    //11.
    [Range(20f, 200f)]
    //12.
    [Tooltip("The health value pf 20 - 200 is for different tank types  ")]
    public int maximumHealth = 200;

    //13.
    [SerializeField]
    private bool dead;

    //14.
    [HideInInspector]
    public string tankClass = "Bruiser";

    //Audio Stuff

    //15.
    //16.

    public AudioSource tankAudioSource;
    public AudioClip shootingAudioclip;

    Rigidbody rb;
    string movementAxisName;
    string turnAxisName;
    float movementInputValue;
    float turnInputValue;
    float originalPitch;

    //Context Menu Functions

    //17.
    [ContextMenu("Difficulty - Normal")]
    void SetDifficultyNormal()
    {
        maximumHealth = 200;
        velocity = 20f;
        tankClass = "Bruiser";
    }

    //18.
    [ContextMenu("Difficulty - DarkSouls")]
    void SetDifficultyDarkSouls()
    {
        maximumHealth = 100;
        velocity = 12f;
        tankClass = "Overwatch";
    }

    //GUIButton Functions
    public void SpawnProp(string propname)
    {
        GameObject temp;
        Material mat = new Material(Shader.Find("Standard"));
        switch (propname)
        {
            case "board":
                Debug.Log("board Instantiated");
                temp = Instantiate(board);
                temp.GetComponent<Renderer>().material = mat;
                mat.color = Color.black;
                break;

            case "oil_drum":
                Debug.Log("oil_drum Instantiated");
                temp = Instantiate(oilDrum);
                temp.GetComponent<Renderer>().material = mat;
                mat.color = Color.black;
                break;

            case "crate":
                Debug.Log("crate Instantiated");
                temp = Instantiate(crate);
                temp.GetComponent<Renderer>().material = mat;
                mat.color = Color.black;
                break;

            case "traffic_cone":
                Debug.Log("traffic_cone Instantiated");
                temp = Instantiate(trafficCone);
                temp.GetComponent<Renderer>().material = mat;
                mat.color = Color.black;
                break;

            case "wheel":
                Debug.Log("wheel Instantiated");
                temp = Instantiate(wheel);
                temp.GetComponent<Renderer>().material = mat;
                mat.color = Color.black;
                break;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.isKinematic = true;

        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void Start()
    {
        movementAxisName = "Vertical";// + playerNumber;
        turnAxisName = "Horizontal";// + playerNumber;
    }

    private void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

        TankShot();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void TankShot()
    {
        if (Input.GetButtonUp("Jump"))
        {
            Transform clone;
            clone = Instantiate(shell1.transform, shootingPosition.position, shootingPosition.rotation);
            clone.GetComponent<Rigidbody>().AddForce(clone.transform.up * shootForce * -1f);
            tankAudioSource.clip = shootingAudioclip;
            tankAudioSource.Play();
        }
    }

    private void Move()
    {
        Vector3 movement = transform.up * movementInputValue * speed * Time.deltaTime * -1f;
        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, 0f, turn);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
