using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CodeEditor : MonoBehaviour
{
    bool isActive;
    public InputField input;
    public Text inputText,text,lineNumber;
    public RectTransform content;

    public float padding = 10;
    float lines = 1;
    public int TextSize { get { return text.fontSize; } set {
            inputText.fontSize = text.fontSize = value;
        } }

    




    void Start()
    {
        
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            ((RectTransform)lineNumber.gameObject.transform).position = new Vector2(((RectTransform)lineNumber.gameObject.transform).position.x, ((RectTransform)text.gameObject.transform).position.y);
            if (Input.mouseScrollDelta.y > 0)
            {
                TextSize++;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                TextSize--;
            }
        }
    }
    public void SetActive(bool active)
    {
        isActive = active;
        gameObject.SetActive(active);
        if (isActive) {
            input.ActivateInputField();
        }
    }
    
    public void OnValueChanged()
    {
        text.text ="<color=red>"+ input.text+"</color>";
        Vector2 v = ((RectTransform)text.gameObject.transform).sizeDelta + new Vector2(2, 2) * padding;
        ((RectTransform)input.gameObject.transform).sizeDelta = content.sizeDelta = new Vector2(
            Mathf.Max(v.x, ((RectTransform)transform).rect.width - padding),
            Mathf.Max(v.y, ((RectTransform)transform).rect.height - padding)
            );
        
        if (lines != GetLines(input.text))
        {
            lines = GetLines(input.text);
            var t= "";
            for(int i =1; i <= lines; i++)
            {
                t += i.ToString()+'\n';
            }
            lineNumber.text = t;
            
        }
    }
    public int GetLines(string s)
    {
        var r = 1;
        foreach (char c in s)
            if (c == '\n')
                r++;
        return r;
    }
}
