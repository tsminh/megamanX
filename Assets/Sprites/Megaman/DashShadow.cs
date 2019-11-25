using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    public Vector2 ShadowOffset;
    public Material ShadowMaterial;
    SpriteRenderer spriteRenderer;
    List<GameObject> shadows;
    bool isDash = false;
    private float max(float a, float b) { return (a > b) ? a : b; }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadows = new List<GameObject>();
        for (int i = 0; i < 5; i++) shadows.Add(new GameObject("shadow" + i));
        int ii = 0;
        shadows.ForEach(x =>
        {
            //x = new GameObject("shadow");
            SpriteRenderer xSpriteRenderer = x.AddComponent<SpriteRenderer>();
            xSpriteRenderer.material = ShadowMaterial;
            xSpriteRenderer.material.color = new Color(xSpriteRenderer.material.color.r, xSpriteRenderer.material.color.g, xSpriteRenderer.material.color.b, 0);
            xSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
            xSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
            ii++;
        });
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            isDash = false;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            isDash = true;
        }
    }
    void LateUpdate()
    {
        //update the position and rotation of the sprite's shadow with moving sprite
        int i = 1;
        shadows.ForEach(x =>
        {
            SpriteRenderer xSpriteRenderer = x.GetComponent<SpriteRenderer>();
            if (!isDash)
            {
                xSpriteRenderer.material.color = new Color(xSpriteRenderer.material.color.r, xSpriteRenderer.material.color.g, xSpriteRenderer.material.color.b, max(xSpriteRenderer.material.color.a - 0.8f * Time.deltaTime, 0));
            }
            else
            {
                xSpriteRenderer.material.color = new Color(xSpriteRenderer.material.color.r, xSpriteRenderer.material.color.g, xSpriteRenderer.material.color.b, max(0.9f - 0.15f * i, 0));
            }


            x.transform.localPosition = transform.localPosition + (Vector3)(ShadowOffset * i * (spriteRenderer.flipX ? 1 : -1));
            x.transform.localRotation = transform.localRotation;
            xSpriteRenderer.flipX = spriteRenderer.flipX;
            xSpriteRenderer.sprite = spriteRenderer.sprite;
            i++;
        });
        
    }
}
