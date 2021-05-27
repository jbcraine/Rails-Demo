using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Every Card with have a face
public class CardFace : MonoBehaviour
{
    Image renderer;
    public Sprite[] faces;
    public Sprite cardBack;

    public int cardIndex;

    public void ToggleFace(bool showFace)
    {
        if (showFace)
            renderer.sprite = faces[cardIndex];
        else
            renderer.sprite = cardBack;
    }

    private void Awake() {
        renderer = GetComponent<Image>();
    }
}
