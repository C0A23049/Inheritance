using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Text_ice : MonoBehaviour
{
    private string[] words;
    [SerializeField] public Text textLabel;
    [SerializeField] private float next_text;
    private bool text_do;
    private string words_enter;
    private bool letter_do;
    public bool Text_do { get { return text_do;  } }
    public bool Return_wait { get { return return_wait; } }
    private bool Return_check;
    private bool return_wait;
    // Start is called before the first frame update
    void Start()
    {
        text_do = false;
        letter_do = false;
        Return_check = false;
        return_wait = false;
    }
    public IEnumerator chat_1letter(string talks)
    {
        text_do = true;
        letter_do = true;
        return_wait = false;
        textLabel.text = "";
        char[] words = talks.ToCharArray();
        float sec;
        sec = next_text;
        words_enter = talks;
        
        foreach (var word in words)
        {
            
            if (Return_check)
            {
                yield break;
            }
            textLabel.text = textLabel.text + word;
            yield return new WaitForSeconds(sec);
        }
        StartCoroutine(chat_enter());
    }
    private IEnumerator chat_enter()
    {
        text_do = true;
        letter_do = false;
        return_wait = false;
        textLabel.text = words_enter;
        yield return null;
        return_wait = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        return_wait = false;
        Return_check = false;
        text_do = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && letter_do)
        {
            Return_check = true;
            StartCoroutine(chat_enter());
        }
    }
}
