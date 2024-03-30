using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTableau : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    public TableauData data;
    private void Start() {
        _name.text = data.name;
        GetComponent<Image>().color = data.found ? Color.green : Color.red;
    }
}
