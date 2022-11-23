using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTransparent : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;

    void OnRenderImage(RenderTexture from, RenderTexture to)
    {
        Graphics.Blit(from, to, m_Material);
    }
}
