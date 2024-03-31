using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Replica {
    public Replica(string personnage, string emotion, string message) {
        this.message = message;
        this.personnage = personnage;
        this.emotion = emotion;
    }

    public string personnage;
    public string message;
    public string emotion;
}

public class TableauManager : MonoBehaviour {
    public TextMeshProUGUI tableauText;
    public Image tableauImage;
    [SerializeField] Image avatar;
    public TableauData data;
    public GameObject choiceButton;
    public Transform parent;
    AudioSource audioSource;
    [SerializeField] List<AudioClip> twainNormals;



    static public TableauManager unique;

    [SerializeField] private float _letterDelay;
    private bool _allTextDisplayed = false;
    private bool _choicesDisplayed = false;

    List<Replica> dialog = new(); //Personnage - Message
    int index = 0;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        unique = this;
        ChangeTableau(data);
    }

    private void Update() {

        if (Input.GetMouseButtonUp(1) && index < dialog.Count - 1) {
            StopAllCoroutines();
            index = dialog.Count - 1;
            DisplayReplica(dialog[index]);
        }

        if (Input.GetMouseButtonDown(0) && !_allTextDisplayed) {

            StopAllCoroutines();
            tableauText.text = dialog[index].message;
            _allTextDisplayed = true;
        }
        else if (Input.GetMouseButtonDown(0) && _allTextDisplayed) {

            index++;
            if (index < dialog.Count) {
                DisplayReplica(dialog[index]);
            }
        }

        if (index == dialog.Count - 1 && _allTextDisplayed && !_choicesDisplayed) {
            SpawnChoices();
            _choicesDisplayed = true;
        }
    }

    public void ChangeTableau(TableauData data) {

        _allTextDisplayed = false;
        _choicesDisplayed = false;

        foreach (Transform child in parent) {
            Destroy(child.gameObject);
        }

        this.data = data;
        StopAllCoroutines();
        tableauImage.sprite = data.name.Contains("Mort") ? Resources.Load<Sprite>("Sprites/Tableaux/Mort") : Resources.Load<Sprite>($"Sprites/Tableaux/{data.name}");

        ParseText();

        DisplayReplica(dialog[index = 0]);


    }

    IEnumerator LetterByLetter(string text) {
        _allTextDisplayed = false;

        tableauText.fontStyle = data.name.Contains("Mort") ? FontStyles.Italic : FontStyles.Normal;

        for (int i = 0; i < text.Length + 1; i++) {
            tableauText.text = text.Substring(0, i);

            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/typewriter"), 0.05f);

            yield return new WaitForSeconds(_letterDelay);
            if (text.Substring(0, i).EndsWith(".") || text.Substring(0, i).EndsWith("?") || text.Substring(0, i).EndsWith("!"))
                yield return new WaitForSeconds(0.5f);
        }
        _allTextDisplayed = true;
    }

    private void SpawnChoices() {

        for (int i = 0; i < data.choices.Count; i++) {
            GameObject choice = Instantiate(choiceButton, parent);
            choice.GetComponent<ChoiceManager>().choiceData = data.choices[i];
        }
    }

    private void DisplayReplica(Replica replica) {

        audioSource.PlayOneShot(Resources.Load<AudioClip>($"Sounds/{dialog[index].personnage}{dialog[index].emotion}"), 1f);
        avatar.sprite = Resources.Load<Sprite>($"Sprites/Personnages/{dialog[index].personnage}{dialog[index].emotion}");
        StartCoroutine(LetterByLetter(dialog[index].message));
    }

    private void ParseText() {
        dialog.Clear();

        string[] lines = data.text.Split('$');

        foreach (var line in lines) {
            string[] parts = line.Split(":");
            string[] character = parts[0].Split("£");

            dialog.Add(new Replica(character[0].Trim(), character[1].Trim(), parts[1].Trim()));
        }
    }
}