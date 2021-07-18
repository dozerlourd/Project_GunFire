using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : SceneObject<GoldManager>
{
    private static int goldAmount;

    public static int GetGoldAmount()
    {
        return goldAmount;
    }

    public static void SetGoldAmount(int value)
    {
        goldAmount += value;
    }

    [SerializeField] Text testText;

    void ShowGoldText()
    {
        testText.text = goldAmount.ToString();
    }

    private void Update()
    {
        ShowGoldText();
    }
}
