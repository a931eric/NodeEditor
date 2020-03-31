using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CommentController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool textExpanded;   // is our commment expanded or not
    private Text commentText;    // Object reference to the Text part of our Prefab
    private LayoutElement commentLayout;   // Object reference to the Layout component of the Panel
    private ContentSizeFitter commentTextSizeFitter;  //Object reference to the ContentSizeFitter in the Text Component
    private float initialHeight;  // Our Prefab Text's Original Height

    void Start()
    {
        // Initalize all our variables
        commentText = GetComponentInChildren<Text>();
        commentLayout = GetComponent<LayoutElement>();
        commentTextSizeFitter = GetComponentInChildren<ContentSizeFitter>();
        initialHeight = commentText.rectTransform.sizeDelta.y;
        textExpanded = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (textExpanded)
            CollapseText();
        else
            ExpandText();
    }

    public void OnPointerDown(PointerEventData eventData)
    { }

    private void CollapseText()
    {
        //turn off the size fitter and reset the Text to initialHeight
        commentTextSizeFitter.enabled = false;
        commentText.rectTransform.sizeDelta = new Vector2(commentText.rectTransform.sizeDelta.x, initialHeight);
        commentLayout.minHeight = initialHeight;
        textExpanded = false;
    }

    private void ExpandText()
    {
        // Turn on the size Fitter, and set the panel's Min height to the new size Fitter
        commentTextSizeFitter.enabled = true;

        // ContextSizeFitter is run in a Co-routine.  So we have to wait till its done resizing
        // We can't just wait until the commentText size is > InitialHeight because the comment might not have overflowed
        // So we set the size to 0, then wait for it to be bigger than 0
        commentText.rectTransform.sizeDelta = new Vector2(commentText.rectTransform.sizeDelta.x, 0);
        StartCoroutine(WaitForSizeChange());
    }

    IEnumerator WaitForSizeChange()
    {
        bool waitFlag = true;
        while (waitFlag)
        {
            if (commentText.rectTransform.sizeDelta.y > 0)
                waitFlag = false;
            yield return null; //wait for next Update
        }
        // ContentFitter got down resizing our Text.. now lets resize the panel
        commentLayout.minHeight = commentText.rectTransform.sizeDelta.y;
        textExpanded = true;
    }

}
