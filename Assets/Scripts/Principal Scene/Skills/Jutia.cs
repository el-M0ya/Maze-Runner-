using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jutia : Skills
{
    /// <summary>
    /// Aumenta su velocidad en x2 en ese turno
    /// </summary>
    public override void UseHability()
    {
        AudioManager.Instance.Play_Audio(TurnManager.Instance.JutiaFast);
        TurnManager.Instance.Skill_UI[(int)Skill.Jutia].SetActive(true);
        base.UseHability();
        TurnManager.Instance._currentplayer.Speed *= 2;
        TurnManager.Instance._currentplayer.Is_Jutia = true;
    }
}
