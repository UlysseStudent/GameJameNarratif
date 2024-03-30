using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Choice choiceData;
    public TableauManager tableauManager;
    public Button button;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        text.text = choiceData.text;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

   public void OnClick()
    {
        TableauManager.unique.ChangeTableau(choiceData.goToTableau);
        
    }

    

   

    
}
