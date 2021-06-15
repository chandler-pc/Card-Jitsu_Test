using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Card prefab = null;
    public Card[] cards = new Card[5];
    public GameObject Deck = null;

    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            Transform pos = this.gameObject.transform.GetChild(i);
            cards[i] = Instantiate(prefab, pos.position + new Vector3(3,0,0), Quaternion.identity);
            cards[i].transform.SetParent(this.gameObject.transform.GetChild(5));
        }
    }
    public IEnumerator NewDeck()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 5; i++)
        {
            Destroy(cards[i].gameObject);
            Transform pos = this.gameObject.transform.GetChild(i);
            cards[i] = Instantiate(prefab, pos.position + new Vector3(3, 0, 0), Quaternion.identity);
            cards[i].transform.SetParent(this.gameObject.transform.GetChild(5));
        }
    }
}
