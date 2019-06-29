using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Sentity playerSentity;
    public GameObject resourceDisplay;
    private RectTransform resourceRT;
    public Vector2 standardSize;
    private float startHealth;
    private Image healthImage;

    private void Awake()
    {
        resourceRT = resourceDisplay.GetComponent<RectTransform>();
        resourceRT.anchoredPosition.Set(0f, 0.5f);
        standardSize = resourceRT.sizeDelta;
        healthImage = resourceDisplay.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSentity = GameObject.FindGameObjectWithTag("Player").GetComponent<Sentity>();
        startHealth = (float) playerSentity.Health;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
    }

    public void UpdateBar()
    {
        float currentHealth = (float)playerSentity.Health;
        float fraction = currentHealth / startHealth;
        Vector3 newSize = standardSize;
        newSize.x = fraction * standardSize.x;
        resourceRT.sizeDelta = newSize;
        SetColour(fraction);
    }

    public void SetColour(float fraction)
    {
        Color newColor = new Color(1f - fraction, fraction, 0f);
        healthImage.color = newColor;
    }
}
