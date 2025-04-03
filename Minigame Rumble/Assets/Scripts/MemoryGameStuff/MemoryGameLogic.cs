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
                CheckMatch();
            }
        }
    }
    public void CheckMatch()
    {
        BoxHandler bxhndlr1 = chosenBoxes[0];
        BoxHandler bxhndlr2 = chosenBoxes[1];

        float expansionDuration = thesetup.GetExpansionDuration();

        SpriteRenderer firstRenderer = bxhndlr1.GetImageRenderer();
        SpriteRenderer secondRenderer = bxhndlr2.GetImageRenderer();

        if (firstRenderer.sprite == secondRenderer.sprite)
        {
            // Found a match
            numMatches++;
            canClick = true;
            chosenBoxes.Clear();
            GameManager.Instance.AddPointToCurrentPlayer();
            GameManager.Instance.EndTurn();
            CheckForWin();
        }
        else
        {
            // Not a match: shrink back images and reset sorting order
            StartCoroutine(ResetUnmatchedCards(bxhndlr1, bxhndlr2, expansionDuration));
            GameManager.Instance.EndTurn();
        }
        
    }

    private IEnumerator ResetUnmatchedCards(BoxHandler box1, BoxHandler box2, float duration)
    {
        yield return new WaitForSeconds(1f); // Wait before shrinking

        // Shrink the images back to their original size
        AnimateScaleTween(box1.GetChildTransform(), box1.origScale, duration);
        AnimateScaleTween(box2.GetChildTransform(), box2.origScale, duration);

        // Reset sorting order so they go back behind the card
        box1.GetImageRenderer().sortingOrder = -2;
        box2.GetImageRenderer().sortingOrder = -2;
        
        // Reset availability
        box1.ResetAvaliability();
        box2.ResetAvaliability();

        // Clear the chosen boxes list *after* animations complete
        yield return new WaitForSeconds(duration);
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
