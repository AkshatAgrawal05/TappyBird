﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public static float offsetX = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BirdScript.instance != null) {
            if (BirdScript.instance.isAlive) {
                MoveTheCamera();
            }
        }
    }

    void MoveTheCamera() {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + 0f;
        transform.position = temp;
    }
}
