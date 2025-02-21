using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MemoryGameLogic : MonoBehaviour
{
    public MemoryGameSetup thesetup;
    public List<GameObject> chosenBoxes = new List<GameObject>();
    private int numMatches;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numMatches = 0;
        thesetup = FindAnyObjectByType<MemoryGameSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoxClicked(GameObject box)
    {
        var imageTransform = box.transform.GetChild(0);
        BoxHandler bh = box.GetComponent<BoxHandler>();

        Vector3 targetScale = bh.origScale * thesetup.GetExpansionMultiplier();
        
        StartCoroutine(AnimateScale(imageTransform, imageTransform.localScale, targetScale, thesetup.GetExpansionDuration()));

        imageTransform.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
        chosenBoxes.Add(box);

        if (chosenBoxes.Count == 2)
        {
            StartCoroutine(DelayedCheckMatch());
        }
    }

    private IEnumerator DelayedCheckMatch()
    {
        yield return new WaitForSeconds(1f);
        CheckMatch();
    }

    private IEnumerator AnimateScale(Transform target, Vector3 startScale, Vector3 endScale, float duration)
    {
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
        SpriteRenderer firstimagename = chosenBoxes[0].transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer secondimagename = chosenBoxes[1].transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (firstimagename.sprite == secondimagename.sprite)
        {
            numMatches++;
            Debug.Log("It's a match!!");
            CheckForWin();
        }
        else
        {
            foreach (GameObject box in chosenBoxes)
            {
                var imageTransform = box.transform.GetChild(0);
                BoxHandler bh = box.GetComponent<BoxHandler>();
                StartCoroutine(AnimateScale(imageTransform, imageTransform.localScale, bh.origScale, thesetup.GetContractionDuration()));
                box.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
                box.GetComponent<BoxHandler>().ResetAvaliability();
            }
        }
        chosenBoxes.Clear();
    }

   

    public void CheckForWin()
    {
        if (numMatches == thesetup.GetNumBoxes()/2)
        {
            Debug.Log("You win!");
        }
    }
}
