using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MemoryGameLogic : MonoBehaviour
{
    public ScreenController sceneNavigator;
    public MemoryGameSetup thesetup;
    public List<BoxHandler> chosenBoxes = new List<BoxHandler>();
    private int numMatches;
    private bool canClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canClick = true;
        numMatches = 0;
        thesetup = FindAnyObjectByType<MemoryGameSetup>();
    }
    
    public void BoxClicked(GameObject box)
    {
        //Flag so that users don't click on more than two boxes at a time
        if (canClick)
        {
            //When a box is clicked, trigger the growing animation and add the box to the chosenBoxes list
            BoxHandler bh = box.GetComponent<BoxHandler>();
            SpriteRenderer imageRenderer = bh.GetImageRenderer();

            Vector3 targetScale = bh.origScale * thesetup.GetExpansionMultiplier();
        
            chosenBoxes.Add(bh);
        
            StartCoroutine(AnimateScale(imageRenderer.transform, imageRenderer.transform.localScale, targetScale, thesetup.GetExpansionDuration()));

            imageRenderer.sortingOrder = 2;
        
            if (chosenBoxes.Count == 2)
            {
                //After a second wait, check for a match
                canClick = false;
                StartCoroutine(DelayedCheckMatch());
            }
        }
    }

    private IEnumerator DelayedCheckMatch()
    {
        yield return new WaitForSeconds(1f);
        CheckMatch();
    }

    private IEnumerator AnimateScale(Transform target, Vector3 startScale, Vector3 endScale, float duration)
    {
        //Logic for animating the image
        float time = 0f;
        while (time < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        target.localScale = endScale;
    }

    public void CheckMatch()
    {
        //Logic for checking to see if you have a match
        BoxHandler bxhndlr1 = chosenBoxes[0];
        BoxHandler bxhndlr2 = chosenBoxes[1];
        
        SpriteRenderer firstRenderer = bxhndlr1.GetImageRenderer();
        SpriteRenderer secondRenderer = bxhndlr2.GetImageRenderer();

        if (firstRenderer.sprite == secondRenderer.sprite)
        {
            //You found a match
            numMatches++;
            CheckForWin();
            canClick = true;
        }
        else
        {
            //Not a match
            //Logic for shrinking the image back to its original position
            foreach (BoxHandler bh in chosenBoxes)
            {
                Transform imageTransform = bh.GetChildTransform();
                StartCoroutine(AnimateScale(imageTransform, imageTransform.localScale, bh.origScale, thesetup.GetContractionDuration()));
                bh.GetImageRenderer().sortingOrder = -2;
                bh.ResetAvaliability();
            }
        }
        chosenBoxes.Clear();
        canClick = true;
    }
    public void CheckForWin()
    {
        int numBoxes = thesetup.GetNumBoxes();
        if (numMatches == numBoxes/2)
        {
            //Placeholder for moving to the next scene when the memory game is finished
            sceneNavigator.LoadScene(7);
        }
    }
}
