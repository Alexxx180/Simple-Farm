using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpGrowth : MonoBehaviour
{
    public Slider Exp;
    public Text Level;
    public Text money;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetNewLevel(int value)
    {
        byte lv = Convert.ToByte(Level.text);
        do
        {
            lv++;
            Level.text = lv.ToString();
            Exp.maxValue = Exp.maxValue + 20 * lv;
            Exp.value = (lv == 255) ? Exp.maxValue : value;
            if (lv == 255)
                break;
        } while (Exp.value + value >= Exp.maxValue);
    }
    private string GoodView(string money)
    {
        if (money.Length - 3 < 1)
            return money;
        for (int i = money.Length; i - 3 > 0; i -= 3)
            money = money.Insert(i - 3, "'");
        return money;
    }
    public void LevelUp(int cost)
    {
        if (ulong.TryParse(money.text.Replace("'", ""), out ulong Money))
        {
            if (Convert.ToInt64(Money) - cost < 0)
                return;
            Money -= Convert.ToUInt32(cost);
            money.text = GoodView(Money.ToString());
            SetNewLevel(Convert.ToInt32(Exp.value));
            if (Level.text == "255")
                gameObject.SetActive(false);
        }
    }
}
