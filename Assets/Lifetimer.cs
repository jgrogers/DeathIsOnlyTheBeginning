using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetimer : MonoBehaviour
{
    [SerializeField] RectTransform upperBulb;
    [SerializeField] RectTransform lowerBulb;
    [SerializeField] RectTransform sandDroppingDown;
    float filledProp = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        DrawLifetimer();
    }

    // Update is called once per frame
    void Update()
    {
       filledProp = Mathf.Max(0.0f, filledProp - Time.deltaTime * 0.03f); 
       DrawLifetimer();
    }
    private void DrawLifetimer() {
        upperBulb.localScale = new Vector3(1.0f, filledProp, 1.0f);
        lowerBulb.localScale = new Vector3(1.0f, 1.0f - filledProp, 1.0f);
        sandDroppingDown.localScale = new Vector3(2.0f/ (1.0f + Mathf.Exp(-filledProp)) - 1f, 1.0f, 1.0f);
    }
}
