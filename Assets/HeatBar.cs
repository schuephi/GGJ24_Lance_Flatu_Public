using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform rt;
    private LanceScript lance;

    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        lance = FindFirstObjectByType<LanceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta= new Vector2(Mathf.Min(lance.CurrentHeat, 100) / 100 * 285, rt.sizeDelta.y);
    }
}
