using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    private Sprite[] plantStages;
    public Sprite[] corns;
    public Sprite[] wheat;
    public Sprite[] carrots;
    public Sprite[] potatoes;
    public Sprite[] wmelons;
    public Image cultureImage;
    public Text money;
    public Text culture;
    public Slider Exp;
    public Text Level;

    int plantStage = 0;
    ulong cost = 5;
    ulong paym = 24;
    uint exp = 5;
    float timeBtwnStages = 1f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlanted)
            return;
        if (timer >= 0)
            timer -= Time.deltaTime;
        if (timer < 0 && plantStage < plantStages.Length - 1)
        {
            timer += timeBtwnStages;
            plantStage++;
            UpdatePlant();
        }    
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == plantStages.Length-1)
                Harvest();
        }
        else
        {
            plantStages = Staging(culture.text);
            Plant();
        }
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
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        if (ulong.TryParse(money.text.Replace("'", ""), out ulong Money))
        {
            Money = Convert.ToUInt64(Money + paym < 9999999999999999999 ? Money + paym : 9999999999999999999);
            money.text = GoodView(Money.ToString());
        }
        if (Level.text == "255")
            return;
        if (Exp.value + exp >= Exp.maxValue)
            SetNewLevel(Convert.ToInt32(Exp.value + exp - Exp.maxValue)); 
        else
            Exp.value += exp;
    }
    private string GoodView(string money)
    {
        if (money.Length - 3 < 1)
            return money;
        for (int i=money.Length;i - 3 > 0; i-=3)
            money = money.Insert(i-3, "'");
        return money;
    }
    void Plant()
    {
        if (ulong.TryParse(money.text.Replace("'", ""), out ulong Money))
        {
            if (Convert.ToInt64(Money) - Convert.ToInt64(cost) < 0)
                return;
            Money -= cost;
            money.text = GoodView(Money.ToString());
        }
        else
            return;
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwnStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
    }
    private Sprite[] Staging(string text)
    {
        switch (text)
        {
            case "Wheat":
                cost = 10;
                paym = 45 + 9 * Convert.ToUInt64(Level.text);
                exp = 15;
                cultureImage.sprite = wheat[wheat.Length - 1];
                timeBtwnStages = 1.5f;
                return wheat;
            case "Carrot":
                cost = 15;
                paym = 75 + 15 * Convert.ToUInt64(Level.text);
                exp = 35;
                cultureImage.sprite = carrots[carrots.Length - 1];
                timeBtwnStages = 2f;
                return carrots;
            case "Potato":
                cost = 25;
                paym = 200 + 40 * Convert.ToUInt64(Level.text);
                cultureImage.sprite = potatoes[potatoes.Length - 1];
                exp = 60;
                timeBtwnStages = 2.5f;
                return potatoes;
            case "Watermelon":
                cost = 40;
                paym = 400 + 80 * Convert.ToUInt64(Level.text);
                exp = 100;
                cultureImage.sprite = wmelons[wmelons.Length - 1];
                timeBtwnStages = 3f;
                return wmelons;
            default:
                cost = 5;
                paym = 20 + 4 * Convert.ToUInt64(Level.text);
                exp = 5;
                cultureImage.sprite = corns[corns.Length - 1];
                timeBtwnStages = 1f;
                return corns;
        }
    }
    public void NewPlant(string plant)
    {
        culture.text = plant;
    }
}
