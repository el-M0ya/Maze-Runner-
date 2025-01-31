using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Skills
{
    /// <summary>
    /// Aumenta su velocidad en x2 en ese turno
    /// </summary>
    public override void UseHability()
    {
        AudioManager.Instance.Play_Audio(TurnManager.Instance.KidLaugh);
        Vector3 Position = TurnManager.Instance._currentplayer.transform.position;
        TurnManager.Instance.Fruit = Instantiate(TurnManager.Instance.KidBomb , Position , new Quaternion());
        
    }
}
