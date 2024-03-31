using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChoiceManager : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Choice choiceData;
    public TableauManager tableauManager;
    public Button button;
    Animator animator;
    AudioSource audioSource;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        text.text = choiceData.text;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

   public void OnClick()
    {
        audioSource.Play();
        
        TableauManager.unique.ChangeTableau(choiceData.goToTableau);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hover", true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Iddle", 0, 0f);
        animator.SetBool("Hover", false);
        

    }
}
