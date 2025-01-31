using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vagabund : Skills
{
     /// <summary>
    /// Los jugadores comenzaran a perder 1 vida durante 3 turnos
    /// </summary>
    public override void UseHability()
    {
         AudioManager.Instance.Play_Audio(TurnManager.Instance.Vagabund);
        TurnManager.Instance.Skill_UI[(int)Skill.Vagabund].SetActive(true);
        for(int i = 0 ; i < TurnManager.Instance.players.Count ; i++)
        {
            if(TurnManager.Instance.players[i] != TurnManager.Instance._currentplayer)
            {
               TurnManager.Instance.players[i].Count_Vagabund = 3;
               TurnManager.Instance.players[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(0 , 100 , 0);
            }
        }
    }
}
