using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    private void Start()
    {
        characters[TurnManager.Instance._currentplayer.PlayerNumber].gameObject.SetActive(true);
    }
}
