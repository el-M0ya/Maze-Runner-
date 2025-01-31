using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Repa : Skills
{
    /// <summary>
    /// Establece la velocidad de los jugadores en 1 durantes un turno, menos la de el mismo
    /// </summary>
    public override void UseHability()
    {
        TurnManager.Instance.MusicRepa.GetComponent<AudioSource>().Play();
        TurnManager.Instance.MusicBack.GetComponent<AudioSource>().Pause();
        TurnManager.Instance.Skill_UI[(int)Skill.Repa].SetActive(true);
        for (int i = 0; i < TurnManager.Instance.players.Count; i++)
        {
            if (TurnManager.Instance.players[i] != TurnManager.Instance._currentplayer)
            {
                TurnManager.Instance.players[i].Is_Repa = true;
                TurnManager.Instance.players[i].Speed = 1;
                TurnManager.Instance.players[i].guachineo.Start();
            }
        }
        TurnManager.Instance._currentplayer.Is_Guachineo = true;
    }
}
