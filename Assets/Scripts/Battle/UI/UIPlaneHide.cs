using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is for hiding UI components when player plane is just at UI location.
/// </summary>
public class UIPlaneHide : MonoBehaviour 
{
    public GameObject player = null;
    public Image uiImage;
    public float dist = 3f;

    void Start()
    {
        if (player == null) player = FindObjectOfType<Player>().gameObject;
    }

    void FixedUpdate()
    {
        float rate = Vector2.Distance(player.transform.position, this.gameObject.transform.position) / dist;
        if (rate > 1f) rate = 1f;
        rate = 0.25f + rate * 0.75f;
        Color c = uiImage.color;
        c.a = rate;
        uiImage.color = c;
    }
}
