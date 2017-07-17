using UnityEngine;
using UnityEngine.UI;

public abstract class Creature : MonoBehaviour
{
    protected void UpdateBar(Text textBar, string value)
    {
        textBar.text = value;
    }

    protected void UpdateBar(Image bar, float currentValue, float maxValue)
    {
        bar.rectTransform.localScale = new Vector3(currentValue / maxValue, 1, 1);
    }

    protected void UpdateBarText(Text barText, float currentValue)
    {
        barText.text = currentValue.ToString();
    }

    protected void UpdateBarText(Text barText, float currentValue, float maxValue)
    {
        barText.text = currentValue + " / " + maxValue;
    }
}