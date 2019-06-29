using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer.sprite = Configuration.Data.Levels[GameFileManager.GameFile.CurrentLevel].Sky;
    }
}
