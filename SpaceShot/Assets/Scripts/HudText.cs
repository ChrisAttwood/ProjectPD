using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudText : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    Vector2 origin;
    Camera cam;

    int life;

    int fontSize;
    float fade = 1f;
    float rise = 0f;


    private void Awake()
    {
        cam = Camera.main;
        transform.SetParent(MainCanvas.instance.transform);
    }

    public void Set(Vector2 origin,string text)
    {
        this.origin = origin;
        textMeshProUGUI.text = text;
        transform.position = cam.WorldToScreenPoint(origin);
        life = 0;
        textMeshProUGUI.fontSize = 0;
    }

   
    

    // Update is called once per frame
    void Update()
    {
        life++;
        rise += 0.01f;
        if (life < 20)
        {
            rise += 0.1f;
            fontSize += 5;
           
            textMeshProUGUI.fontSize = fontSize;
        }
        else if(fontSize>0f)
        {
            fontSize-=1;
            textMeshProUGUI.fontSize = fontSize;
        }
       // else
        {
            fade -= 0.01f;

            textMeshProUGUI.color = new Color(1f, 1f, 1f, fade);

            if (fade <= 0f)
            {
                Destroy(gameObject);
            }
        }


        //var pos = new Vector2(origin.x, origin.y + life / 25f);
        var pos = new Vector2(origin.x, origin.y + rise);
        



        transform.position = cam.WorldToScreenPoint(pos);
    }
}
