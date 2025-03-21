using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

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
    
    public void AnimateScaleTween(Transform target, Vector3 endScale, float duration)
    {
        target.DOScale(endScale, duration);
    }
    
    public void BoxClicked(GameObject box)
    {
        //Flag so that users don't click on more than two boxes at a time
        if (canClick)
        {
            //When a box is clicked, trigger the growing animation and add the box to the chosenBoxes list
            BoxHandler bh = box.GetComponent<BoxHandler>();
            SpriteRenderer imageRenderer = bh.GetImageRenderer();

            float expansionMultipler = thesetup.GetExpansionMultiplier();
            float expansionDuration = thesetup.GetExpansionDuration();
            
            Vector3 targetScale = bh.origScale * expansionMultipler;
            chosenBoxes.Add(bh);
        
            AnimateScaleTween(imageRenderer.transform, targetScale, expansionDuration);

            imageRenderer.sortingOrder = 2;
        
            if (chosenBoxes.Count == 2)
            {
                //After a second wait, check for a match
                canClick = false;
                Debug.Log("Delayed Checking Match");
                DelayedCheckMatch();
            }
        }
    }

    /*private static readonly WaitForSeconds oneSecondDelay = new WaitForSeconds(1f);
    private IEnumerator DelayedCheckMatch()
    {
        yield return oneSecondDelay;
        CheckMatch();
    }*/

    private void DelayedCheckMatch()
    {
        DOVirtual.DelayedCall(0.2f, CheckMatch);
    }
    
    public void CheckMatch()
    {
        //Logic for checking to see if you have a match
        BoxHandler bxhndlr1 = chosenBoxes[0];
        BoxHandler bxhndlr2 = chosenBoxes[1];
        
        float expansionDuration = thesetup.GetExpansionDuration();
        
        SpriteRenderer firstRenderer = bxhndlr1.GetImageRenderer();
        SpriteRenderer secondRenderer = bxhndlr2.GetImageRenderer();

        if (firstRenderer.sprite == secondRenderer.sprite)
        {
            //You found a match
            numMatches++;
            //CheckForWin();
            DOVirtual.DelayedCall(1f, CheckForWin);
            canClick = true;
        }
        else
        {
            //Not a match
            //Logic for shrinking the image back to its original position
            foreach (BoxHandler bh in chosenBoxes)
            {
                Transform imageTransform = bh.GetChildTransform();
                //StartCoroutine(AnimateScale(imageTransform, imageTransform.localScale, bh.origScale, thesetup.GetContractionDuration()));
                AnimateScaleTween(imageTransform, bh.origScale,expansionDuration);
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
