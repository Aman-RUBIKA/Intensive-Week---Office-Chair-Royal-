using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boom : MonoBehaviour
{
    public SpriteRenderer sprite;
    private float endValue;
    public float maxCountdown;
    public float countdown;
    private float duration = 1;
    public float lerpDuration = 0.5f;
    public float damage = 5;
    
    void Start()
    {
        countdown = maxCountdown;
    }
    
    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpriteFade(sprite, 0, lerpDuration));
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<AI>() != null)
        {
            col.GetComponent<AI>().hp -= damage;
        }
        else if (col.GetComponent<HealthPC>())
        {
            HealthPC.instance.callWhenDamagedPC(damage);
        }
    }

    public IEnumerator SpriteFade(
        SpriteRenderer sr, 
        float endValue, 
        float duration)
    {
        float elapsedTime = 0;
        float startValue = sr.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            Debug.Log(newAlpha);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            Debug.Log(sr.color);
            yield return null;
        }
    }
    
}
