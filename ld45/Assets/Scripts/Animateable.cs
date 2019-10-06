using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animateable : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    public Sprite []sprites;
    static private SpriteRenderer renderer;
    int currentSprite;
    public void Animate(float time,int frameCount)
    {
        coroutine = AnimateCo(time, frameCount);
        StartCoroutine(coroutine);
        coroutine = null;
    }
    public void CeaseAnimation()
    {
        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
        }
    }
    private IEnumerator AnimateCo(float time,int frameCount)
    {
        for(int i = 0;i<frameCount;i++)
        {
            yield return new WaitForSeconds(time / (frameCount));
            currentSprite = (currentSprite+1)% sprites.Length;
            renderer.sprite = sprites[currentSprite];
        }
        yield return new WaitForSeconds(time / (frameCount));
    }
    private void Awake()
    {
        coroutine = null;
        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
