using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject palierP;
    [SerializeField] DisplayTableau tableauP;

    [SerializeField] TableauData initialTableau;
    private int index = 0;

    private void Start() {
        DisplayTableaux();
    }

    private void DisplayTableaux() {
        var palier = Instantiate(palierP, transform);
        DisplayTableau(initialTableau, palier);
    }

    private void DisplayTableau(TableauData data, GameObject currentPalier) {
        if (index++ > 40)
            return;
        var currentTableau = Instantiate(tableauP, currentPalier.transform);
        currentTableau.data = data;

        var palier = Instantiate(palierP, transform);

        foreach(var choice in data.choices) {
            if (choice.goToTableau.name != "Mort" || !choice.goToTableau.intermediate)
                DisplayTableau(choice.goToTableau, palier);
        }
    }
}
