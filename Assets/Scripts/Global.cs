using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Global : MonoBehaviour
{
    public GameObject positions = null;
    public bool canMove = true;
    public TextMeshProUGUI winner = null;
    public MiniCard prefab;
    float[] contYP = new float[3] {0,0,0};
    float[] contYE = new float[3] {0,0,0};
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void SetMoveState(bool cm)
    {
        canMove = cm;
    }
    public IEnumerator Play()
    {
        yield return new WaitForSeconds(0.3f);
        Card playerCard = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Card>();
        Card enemyCard = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Card>();
        int typePlayer = playerCard.i;
        int typeEnemy = enemyCard.i;
        int valPlayer = playerCard.val;
        int valEnemy = enemyCard.val;
        Color colorPlayer = playerCard.colorCard.color;
        Color colorEnemy = enemyCard.colorCard.color;
        if (typePlayer == typeEnemy)
        {
            if (valPlayer > valEnemy)
            {
                MiniCard cm = Instantiate(prefab, PosMC(0, typePlayer) + new Vector3(0, contYP[typePlayer], 0), Quaternion.identity);
                contYP[typePlayer] -= 1.6f;
                cm.CreateMiniCard(typePlayer, colorPlayer);
                SetParentMC(0, typePlayer, cm);
            }
            else if (valPlayer < valEnemy)
            {
                MiniCard cm = Instantiate(prefab, PosMC(1, typeEnemy) + new Vector3(0, contYE[typeEnemy], 0), Quaternion.identity);
                contYE[typeEnemy] -= 1.6f;
                cm.CreateMiniCard(typeEnemy, colorEnemy);
                SetParentMC(1, typeEnemy, cm);
            }
        }
        else
        {
            // 0:water , 1:fire , 2:snow
            if (typePlayer == typeEnemy - 1 || typePlayer - 2 == typeEnemy)
            {
                MiniCard cm = Instantiate(prefab, PosMC(0, typePlayer) + new Vector3(0, contYP[typePlayer], 0), Quaternion.identity);
                contYP[typePlayer] -= 1.6f;
                cm.CreateMiniCard(typePlayer, colorPlayer);
                SetParentMC(0, typePlayer, cm);
            }
            if (typeEnemy == typePlayer - 1 || typeEnemy - 2 == typePlayer)
            {
                MiniCard cm = Instantiate(prefab, PosMC(1, typeEnemy) + new Vector3(0, contYE[typeEnemy], 0), Quaternion.identity);
                contYE[typeEnemy] -= 1.6f;
                cm.CreateMiniCard(typeEnemy, colorEnemy);
                SetParentMC(1, typeEnemy, cm);
            }
        }
        Win();
        yield return new WaitForSeconds(1.3f);
        Destroy(enemyCard.gameObject);
        canMove = true;
    }
    Vector3 PosMC(int poe, int i)
    {
        return positions.transform.GetChild(poe).GetChild(i).position;
    }
    void SetParentMC(int poe, int i, MiniCard mc)
    {
        mc.transform.SetParent(positions.transform.GetChild(poe).GetChild(i));
    }
    public void Win()
    {
        if (WinSameElement(0) || Win3Elements(0))
        {
            winner.text = "Player Win";
            Time.timeScale = 0;
        }
        if (WinSameElement(1) || Win3Elements(1))
        {
            winner.text = "Enemy Win";
            Time.timeScale = 0;
        }
    }
    bool WinSameElement(int k)
    {
        Color[] arrColor = new Color[5] { Color.cyan, Color.green, Color.red, Color.blue, Color.yellow};
        int t = 0;
        for(int i = 0; i < 3; i++)
        {
            int[] arrInt = new int[5] { 0, 0, 0, 0, 0 };
            if (positions.transform.GetChild(k).GetChild(i).childCount >= 3)
            {
                for (int m = 0; m < positions.transform.GetChild(k).GetChild(i).childCount; m++)
                {
                    MiniCard mc = positions.transform.GetChild(k).GetChild(i).GetChild(m).gameObject.GetComponent<MiniCard>();
                    int x = Array.IndexOf(arrColor, mc.colorCard.color);
                    if (x != -1)
                    {
                        arrInt[x] += 1;
                    }
                }
                foreach (int p in arrInt)
                {
                    if (p != 0)
                    {
                        t++;
                    }
                }
            }
            if (t >= 3)
            {
                return true;
            }
        }
        return false;
    }
    bool Win3Elements(int k)
    {
        int t = 0;
        Color[] arrColor = new Color[5] { Color.cyan, Color.green, Color.red, Color.blue, Color.yellow };
        int[,] arrWFS = new int[3,5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        int[] arrNumberColor = new int[5] { 0, 0, 0, 0, 0};
        int[] numberElements = new int[3] {0,0,0};
        for (int i = 0; i < 3; i++)
        {
            numberElements[i] = positions.transform.GetChild(k).GetChild(i).childCount;
        }
        if(numberElements[0]!= 0 && numberElements[1] != 0 && numberElements[2] != 0)
        {
            for(int i = 0; i < 3; i++)
            {
                t = 0;
                for (int j = 0; j < positions.transform.GetChild(k).GetChild(i).childCount;j++)
                {
                    MiniCard mc = positions.transform.GetChild(k).GetChild(i).GetChild(j).gameObject.GetComponent<MiniCard>();
                    int x = Array.IndexOf(arrColor, mc.colorCard.color);
                    arrWFS[i,x] += 1;
                }
                for(int p = 0; p<3;p++)
                {
                    for(int q = 0; q < 5; q++)
                    {
                        if(arrWFS[p,q] != 0)
                        {
                            arrNumberColor[q] = 1;
                        }
                    }
                }
                for(int l = 0; l < 5; l++)
                {
                    if (arrNumberColor[l] != 0)
                    {
                        t++;
                    }
                }
                if (t >= 3)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
