using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableauManager : MonoBehaviour
{
    public TextMeshProUGUI tableauText;
    public Image tableauImage;
    public TableauData tableauData;
    public GameObject choiceButton;
    public Transform parent;
    static public TableauManager unique;
    // Start is called before the first frame update
    void Start()
    {
        unique = this;
        ChangeTableau(tableauData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTableau(TableauData data)
    {

        tableauImage.sprite = Resources.Load<Sprite>($"Sprites/{data.name}");
        tableauText.text = data.text;
        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        

        for (int i = 0; i < data.choices.Count; i++)
        {
            GameObject choice = Instantiate(choiceButton, parent);
            choice.GetComponent<ChoiceManager>().choiceData = data.choices[i];
             
        }
      
    }
}
