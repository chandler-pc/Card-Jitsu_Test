using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public Global global = null;
    public Enemy enemy = null;
    public Player player = null;
    public SpriteRenderer img = null;
    public int val = 0;
    private TextMeshPro valText = null;
    public Sprite[] arrImg = new Sprite[3];
    public string[] cardTypes = new string[3] {"water","fire","snow"};
    public string type = "water";
    public SpriteRenderer colorCard = null;
    public int i = 0;
    public Color[] colors = new Color[5] { Color.cyan, Color.green, Color.red, Color.blue, Color.yellow};
    private void Start()
    {
        colorCard.color = colors[Random.Range(0, 5)];
        valText = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        val = Random.Range(2, 13);
        valText.text = val.ToString();
        img = GetComponent<SpriteRenderer>();
        i = Random.Range(0, 3);
        img.sprite = arrImg[i];
        type = cardTypes[i];
        global = GameObject.Find("Global").GetComponent<Global>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnMouseUp()
    {
        if (global.canMove)
        {
            //this.gameObject.transform.SetParent(global.gameObject.transform.GetChild(0));
            this.gameObject.transform.position = global.gameObject.transform.GetChild(0).position;
            this.gameObject.transform.SetParent(global.gameObject.transform.GetChild(0).transform);
            global.SetMoveState(!global.canMove);
            StartCoroutine(enemy.MakePlay());
        }
    }
}
