using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSouls : Creature {

    int maxSouls = int.MaxValue;
    int currentSouls = 0;

    private Text soulText;

    private void Start()
    {
        soulText = GameObject.FindGameObjectWithTag(StringCollection.SOUL_TEXT).GetComponent<Text>();
        UpdateSoulBar();
    }

    public void ChangeSoulCount(int soulCount)
    {
        currentSouls = Globals.ChangeValue(soulCount, currentSouls, maxSouls);
        UpdateSoulBar();
    }

    void UpdateSoulBar()
    {
        UpdateBar(soulText, currentSouls.ToString());
    }
}
