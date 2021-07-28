using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealCulture : MonoBehaviour
{
    public Text money;
    public ulong cost;
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

    public void Reveal(Button btn)
    {
        if (ulong.TryParse(money.text.Replace("'", ""), out ulong Money))
        {
            if (Convert.ToInt64(Money) - Convert.ToInt64(cost) < 0)
                return;
            Money -= cost;
            money.text = GoodView(Money.ToString());
            btn.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
