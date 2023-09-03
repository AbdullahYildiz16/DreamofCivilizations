using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private int frameDelay;
    [SerializeField] private bool spriteSheetAnim;

    private int _idx;
    private void Awake()
    {
        if(spriteSheetAnim) StartCoroutine(SpriteEnum());
    }

    private void Update()
    {
        var dir = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-dir);
    }

    IEnumerator SpriteEnum()
    {
        while (true)
        {
            _idx++;
            if (_idx >= sprites.Count) _idx = 0;
            yield return new WaitForSeconds(0.02f * frameDelay);
            _spriteRenderer.sprite = sprites[_idx];
        }
    }
}
