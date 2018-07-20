using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Copy : MonoBehaviour
{
    
    public RenderTextureEvent onCreateRt;
    RenderTexture rt;

    private void OnDestroy()
    {
        if (rt != null)
            rt.Release();
    }

    void CreateRenderTexture(RenderTexture source)
    {
        rt = new RenderTexture(source.width, source.height, 0, source.format);
        rt.Create();
        onCreateRt.Invoke(rt);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (rt == null)
            CreateRenderTexture(source);
        Graphics.CopyTexture(source, rt);
        Graphics.Blit(source, destination);
    }

    [System.Serializable]
    public class RenderTextureEvent : UnityEvent<RenderTexture> { }
}
