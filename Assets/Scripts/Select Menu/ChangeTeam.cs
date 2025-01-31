using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTeam : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public static int _colorindex = 0;
   public GameObject color;
    
    public void Next_Color()
    {
        if(colors.Count == 0) Debug.LogError("No hay colores asignados");
        _colorindex += 1;
        if(_colorindex == colors.Count) _colorindex = 0;
        color.gameObject.GetComponent<SpriteRenderer>().color = colors[_colorindex];
    }
    public void Back_Color()
    {
        if(colors.Count == 0) Debug.LogError("No hay colores asignados");
        _colorindex -= 1;
        if(_colorindex == -1) _colorindex = colors.Count - 1;
        color.gameObject.GetComponent<SpriteRenderer>().color = colors[_colorindex];
    }
    public void ToBlack()
    {
        _colorindex = 0;
        color.gameObject.GetComponent<SpriteRenderer>().color = colors[_colorindex];
    }

}
