using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCard : MonoBehaviour
{
    public SpriteRenderer colorCard = null;
    private SpriteRenderer sr = null;
    public int t = 0;
    public Sprite[] arrImg = new Sprite[3];

    public void CreateMiniCard(int t, Color color)
    {
        sr = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        colorCard = this.gameObject.transform.GetComponent<SpriteRenderer>();
        sr.sprite = arrImg[t];
        colorCard.color = color;
    }
}
