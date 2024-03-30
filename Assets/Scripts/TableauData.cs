using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tableau", menuName = "New Tableau")]
public class TableauData : ScriptableObject
{
    public Sprite image;
    public string text;
    public List<Choice> choices;

}
