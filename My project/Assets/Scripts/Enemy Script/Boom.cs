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
    public float lerpDuration = 0.5f;
    public float damage = 5;
    
    void Start()
    {
        countdown = maxCountdown;
        Destroy(this.gameObject, countdown + 1);
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
        if (col.GetComponent<HealthEnemy>() != null)
        {
            col.GetComponent<HealthEnemy>().currentHealth -= damage;
        }
        else if (col.GetComponent<HealthPC>())
        {
            col.GetComponent<HealthPC>().callWhenDamagedPC(damage);
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
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }
    }
    
}
