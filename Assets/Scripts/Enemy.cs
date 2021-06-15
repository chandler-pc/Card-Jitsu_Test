using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player = null;
    public Global global = null;
    public Card prefab = null;
    public Card[] cardsE = new Card[5];

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            cardsE[i] = prefab;
        }
    }
    public IEnumerator MakePlay()
    {
        yield return new WaitForSeconds(0.5f);
        int i = Random.Range(0, 5);
        Card card = Instantiate(cardsE[i], global.transform.GetChild(1).transform.position,Quaternion.identity);
        card.gameObject.transform.SetParent(global.gameObject.transform.GetChild(1).transform);
        StartCoroutine(global.Play());
        StartCoroutine(player.NewDeck());
    }
}
