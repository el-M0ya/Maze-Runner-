using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    [NonSerialized]
    public static List<Character> PlayersToPlay = new List<Character>();
    public static List<int> intPlayers = new List<int>();


    public void Add()
    {
        ChangeCharacter.Instance.ListPlayers[ChangeCharacter.Instance._playerindex].gameObject.SetActive(false);
        PlayersToPlay.Add(ChangeCharacter.Instance.ListPlayers[ChangeCharacter.Instance._playerindex]);
        intPlayers.Add(ChangeCharacter.Instance.ListPlayers[ChangeCharacter.Instance._playerindex].PlayerNumber);
        ChangeCharacter.Instance.ListPlayers.Remove(ChangeCharacter.Instance.ListPlayers[ChangeCharacter.Instance._playerindex]);

        if(ChangeCharacter.Instance._playerindex == ChangeCharacter.Instance.ListPlayers.Count) ChangeCharacter.Instance._playerindex -= 1;
        
        ChangeCharacter.Instance.NextPlayer();
    }
    public void Remove()
    {
        if(PlayersToPlay.Count == 0) return;
        if(ChangeCharacter.Instance.ListPlayers.Count != 0)ChangeCharacter.Instance.ListPlayers[ChangeCharacter.Instance._playerindex].gameObject.SetActive(false);
        ChangeCharacter.Instance.ListPlayers.Add(PlayersToPlay[PlayersToPlay.Count - 1]);
        intPlayers.RemoveAt(intPlayers.Count-1);
        PlayersToPlay.Remove(PlayersToPlay[PlayersToPlay.Count - 1]);
        ChangeCharacter.Instance._playerindex = ChangeCharacter.Instance.ListPlayers.Count - 1;
        ChangeCharacter.Instance.Show();
    }

}
