using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TableauManager : MonoBehaviour
{
    public TextMeshProUGUI tableauText;
    public Image tableauImage;
    public TableauData data;
    public GameObject choiceButton;
    public Transform parent;

    static public TableauManager unique;

    [SerializeField] private float _letterDelay;
    [SerializeField] private float _spawnButtonDelay;
    private bool _allTextDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        unique = this;
        ChangeTableau(data);
    }

    private void Update() {

        if (Input.GetMouseButton(0) && !_allTextDisplayed) {

            StopAllCoroutines();
            tableauText.text = data.text;
            StartCoroutine(SpawnChoices());
            _allTextDisplayed = true;
        }

    }

    public void ChangeTableau(TableauData data) {
        _allTextDisplayed = false;
        foreach (Transform child in parent) {
            Destroy(child.gameObject);
        }

        this.data = data;
        StopAllCoroutines();
        tableauImage.sprite = Resources.Load<Sprite>($"Sprites/{data.name}");

        StartCoroutine(LetterByLetter(data.text));
    }

    IEnumerator LetterByLetter(string text) {
        for (int i = 0; i<text.Length; i++) {
            tableauText.text = text.Substring(0, i);

            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/typewriter"));

            yield return new WaitForSeconds(_letterDelay);
            if (text.Substring(0, i).EndsWith("."))
                yield return new WaitForSeconds(0.5f);
        }
        _allTextDisplayed = true;
        StartCoroutine(SpawnChoices());
    }

    IEnumerator SpawnChoices() {
        
        for (int i = 0; i < data.choices.Count; i++)
        {
            GameObject choice = Instantiate(choiceButton, parent);
            choice.GetComponent<ChoiceManager>().choiceData = data.choices[i];
            yield return new WaitForSeconds(_spawnButtonDelay);
        }
    }
}
