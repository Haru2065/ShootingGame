using UnityEngine;

public class BackgroundOffsetScroll : MonoBehaviour
{
    [SerializeField, Header("背景のスクロール速度")]
    private float scrollSpeed;

    private Renderer rend;
    private Vector2 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //縦に指定した速度でスクロールし続ける
        float offsetY = Time.time * scrollSpeed;
        rend.sharedMaterial.mainTextureOffset = new Vector2(0, offsetY);

    }
}
