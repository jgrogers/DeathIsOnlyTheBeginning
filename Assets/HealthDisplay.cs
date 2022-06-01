using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject target;
    public float healthProp = 1.0f;
    [SerializeField] RectTransform HealthBar;
    [SerializeField] Camera mainCamera;
    private float fullWidth;
    private float fullHeight;
    // Start is called before the first frame update
    void Start() {
        fullWidth = HealthBar.rect.width;
        fullHeight = HealthBar.rect.height;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        HealthBar.sizeDelta = new Vector2(healthProp * fullWidth, fullHeight);
        Vector3 healthbarPosition = new Vector3(target.transform.position.x, target.transform.position.y + 2.0f, target.transform.position.z);
        Vector3 screenPos = mainCamera.WorldToScreenPoint(healthbarPosition);
        transform.position = screenPos;
        //GetComponent<RectTransform>().localPosition = screenPos;

    }
}
