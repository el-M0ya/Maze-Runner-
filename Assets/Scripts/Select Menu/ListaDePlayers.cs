using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players
{
    Jutia , Repa , Vagabund , Drunk
}

public class ListaDePlayers : MonoBehaviour
{
    public List<Character> Players_Disponibles = new List<Character>();

    public  List<Character> CreateListPlayers()
    {
         List<Character> Players = new List<Character>();
        for (int i = 0; i < SelectPlayer.PlayersToPlay.Count; i++)
        {
            Players.Add(Players_Disponibles[SelectPlayer.intPlayers[i]]);
        }
        if(Players.Count == 0)
        {
            UnityEngine.Debug.LogError("No hay personajes para jugar");
        }

         return Players;
    }
}
