﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageBehaviour : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshPro text;
    Color alpha;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        alphaSpeed = 2.0f;
        destroyTime = 0.5f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        DestroyObject();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    public void DestroyObject()
    {
        Destroy(gameObject,destroyTime);
    }
}