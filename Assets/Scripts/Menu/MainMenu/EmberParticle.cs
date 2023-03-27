using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmberParticle : MonoBehaviour
{
    public float fadeOutTime;
    [SerializeField] float speedModifier;
    float speed;
    float timeElapsed;

    Image image;
    Color baseColor;
    
    void OnEnable()
    {
        image = GetComponent<Image>();
        baseColor = image.color;

        float launchAngle = Random.Range(-10, 10);
        fadeOutTime = Random.Range(2.5f, 4.5f);
        speed = speedModifier * fadeOutTime;
        
        transform.rotation = Quaternion.Euler(0f, 0f, launchAngle);
        Destroy(gameObject, fadeOutTime);
    }

    void Update()
    {
        transform.position += transform.up * Time.fixedDeltaTime * speed;
        transform.Rotate(0f, 0f, Random.Range(-0.2f, 0.2f));

        image.color = Color.Lerp(baseColor, Color.clear, Mathf.Lerp(0, 1, timeElapsed / fadeOutTime));
        timeElapsed += Time.deltaTime;
    }
}
