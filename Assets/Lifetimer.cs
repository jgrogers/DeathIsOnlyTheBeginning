using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifetimer : MonoBehaviour
{
    [SerializeField] RectTransform upperBulb;
    [SerializeField] RectTransform lowerBulb;
    [SerializeField] RectTransform sandDroppingDown;
    [SerializeField] Image backgroundImage;
    float filledProp = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        DrawLifetimer();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetFilledProp(float filledProp) {
        this.filledProp = filledProp;
        DrawLifetimer();
    }
    private void DrawLifetimer() {
        upperBulb.localScale = new Vector3(1.0f, filledProp, 1.0f);
        lowerBulb.localScale = new Vector3(1.0f, 1.0f - filledProp, 1.0f);
        sandDroppingDown.localScale = new Vector3(2.0f/ (1.0f + Mathf.Exp(-filledProp)) - 1f, 1.0f, 1.0f);
    }
    public void SetBackgroundColor(Color color) {
        backgroundImage.color = color;
    }
}
