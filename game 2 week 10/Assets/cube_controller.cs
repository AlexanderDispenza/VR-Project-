﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
    }
    void RotateGameObject(){



            gameObject.transform.Rotate(0, 100 * Time.deltaTime, 0);
          
}