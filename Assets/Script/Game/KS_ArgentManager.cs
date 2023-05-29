using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KS_ArgentManager : MonoBehaviour
{
    public int argentDuJoueur;

    public int tortilla;
    public int tomate;
    public int ognon;
    public int salade;
    public int brocheakebab;
    public int sauceblanche;
    public int saucealgerienne;
    public int saucesamourai;
    public int painakebab;

    public Text nombreDargentAffiche;
    public Text tortillaa;
    public Text tomatee;
    public Text ognonn;
    public Text saladee;
    public Text brocheakebabb;
    public Text sauceblanchee;
    public Text saucealgeriennee;
    public Text saucesamouraii;
    public Text painakebabb;
    public GameObject nepeuxpasacheter;

    public void Update()
    {
        nombreDargentAffiche.text = "Argent que tu as :" + argentDuJoueur;
        tortillaa.text = "Tortilla :" + tortilla;
        tomatee.text = "Tomate :" + tomate;
        ognonn.text = "Ognon :" + ognon;
        saladee.text = "Salade :" + salade;
        brocheakebabb.text = "Broche à Kebab :" + brocheakebab;
        sauceblanchee.text = "Sauce Blanche :" + sauceblanche;
        saucealgeriennee.text = "Sauce Algérienne :" + saucealgerienne;
        saucesamouraii.text = "Sauce Samourai :" + saucesamourai;
        painakebabb.text = "Pain à Kebab :" + painakebab;
    }


    public void TortillaButton()
    {
        if (argentDuJoueur >= 10)
        {
            tortilla += 5;
            argentDuJoueur -= 10;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }
    }

    public void TomateButton()
    {
        if (argentDuJoueur >= 5)
        {
            tomate += 2;
            argentDuJoueur -= 5;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }

    }
    
    public void OgnonButton()
    {
        if (argentDuJoueur >= 6)
        {
            ognon += 3;
            argentDuJoueur -= 6;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }

    }

    public void SaladeButton()
    {
        if (argentDuJoueur >= 10)
        {
            salade += 2;
            argentDuJoueur -= 10;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }

    }

    public void BrocheAKebabButton()
    {
        if (argentDuJoueur >= 50)
        {
            brocheakebab += 1;
            argentDuJoueur -= 50;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }


    }

    public void SauceBlancheButton()
    {
        if (argentDuJoueur >= 8)
        {
            sauceblanche += 1;
            argentDuJoueur -= 8;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }
    }

    public void SauceAlgerienneButton()
    {
        if (argentDuJoueur >= 9)
        {
            saucealgerienne += 1;
            argentDuJoueur -= 9;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }
    }

    public void SauceSamouraiButton()
    {
        if (argentDuJoueur >= 5)
        {
            saucesamourai += 1;
            argentDuJoueur -= 5;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }

    }

    public void PainAKebabButton()
    {
        if (argentDuJoueur >= 20)
        {
            painakebab += 10;
            argentDuJoueur -= 20;
        }
        else
        {
            Debug.Log("ta pas asser dargent");
            StartCoroutine(nePeuxPasAcheter());
        }

    }

    IEnumerator nePeuxPasAcheter()
    {
        nepeuxpasacheter.SetActive(true);
        yield return new WaitForSeconds(4);
        nepeuxpasacheter.SetActive(false);
    }
}
