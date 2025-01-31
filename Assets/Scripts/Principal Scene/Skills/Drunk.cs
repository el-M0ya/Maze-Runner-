using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Drunk : Skills
{
    /// <summary>
/// Los jugadores, menos el mismo , tienen el mapa patas arriba
/// </summary>
    public override void UseHability()
    {
        AudioManager.Instance.Play_Audio(TurnManager.Instance.Drunk_Boing);
        TurnManager.Instance.Skill_UI[(int)Skill.Drunk].SetActive(true);
      for(int i = 0 ; i < TurnManager.Instance.players.Count ; i++)
        {
            if(TurnManager.Instance.players[i] != TurnManager.Instance._currentplayer)
            {
               TurnManager.Instance.players[i].Is_Drunk = true;

            }
        }
    }
}
