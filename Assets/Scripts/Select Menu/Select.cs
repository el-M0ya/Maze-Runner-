
using TMPro;
using UnityEngine;

public class Select : MonoBehaviour
{
    public int number = 8;
    public TextMeshProUGUI textMeshPro;

    public void Add()
    {
        if(number <= 256) number += 1;
        ShowNumber();
    }
    public void Rest()
    {
        if(number > 8) number -= 1;
        ShowNumber();
    }
    public void ShowNumber()
    {
        textMeshPro.text = number.ToString();
    }
}
