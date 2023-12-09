using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public Image image;

    public Sprite[] sprites;
    public float speed = .035f;

    private int spriteIndex;
    Coroutine anim;
    private void Awake()
    {
        StartCoroutine(PlayAnim());
    }
    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(speed);
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        image.sprite = sprites[spriteIndex];
        spriteIndex += 1;
        anim = StartCoroutine(PlayAnim());
    }
}
