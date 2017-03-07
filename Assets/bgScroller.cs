﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgScroller : MonoBehaviour {
								    
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector2 savedOffset;
    private Vector3 startPosition;
    
    void Start ()
    {
        startPosition = transform.position;
        savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
    }
    
    void Update ()
    {
        float x = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ * 4);
        x = x / tileSizeZ;
        x = Mathf.Floor (x);
        x = x / 4;
        Vector2 offset = new Vector2 (x, savedOffset.y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.back * newPosition;
    }
    
    void OnDisable () {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
    }
}
