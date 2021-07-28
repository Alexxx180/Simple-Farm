using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealField : MonoBehaviour
{
    public GameObject expGrowth;
    public Text money;
    int slot = 0;
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private string GoodView(string money)
    {
        if (money.Length - 3 < 1)
            return money;
        for (int i = money.Length; i - 3 > 0; i -= 3)
            money = money.Insert(i - 3, "'");
        return money;
    }
    public void BuyAfield(int cost)
    {
        uint costs = Convert.ToUInt32(cost);
        if (ulong.TryParse(money.text.Replace("'", ""), out ulong Money))
        {
            if (Convert.ToInt64(Money) - cost < 0)
                return;
            Money -= costs;
            money.text = GoodView(Money.ToString());
            objects[slot].SetActive(true);
            slot++;
            if (slot >= objects.Length)
            {
                gameObject.SetActive(false);
                expGrowth.SetActive(true);
            }
        }
    }
}
