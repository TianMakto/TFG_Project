using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteData
{
    public Sprite sprite;
    [Range(0, 100)]
    public float probability;
}


public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField]
    private SpriteData[] sprites;

    void Start()
    {
        SpriteRenderer SpriteToRandom = GetComponent<SpriteRenderer>();
        float porcentage = 0;

        for (int i = 0; i < sprites.Length; i++)
        {
            float newPorcentage = Random.Range(0, sprites[i].probability);

            if (newPorcentage > porcentage)
            {
                SpriteToRandom.sprite = sprites[i].sprite;
                porcentage = newPorcentage;
            }
        }
    }
}
