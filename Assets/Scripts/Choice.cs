using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Choice", menuName = "New Choice")]
public class Choice : ScriptableObject
{
    public TableauData goToTableau;
    public string text;
}
