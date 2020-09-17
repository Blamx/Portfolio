using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProccess : MonoBehaviour
{
    public RenderTexture bright;
    public RenderTexture blur;
    public RenderTexture edge;
    public Material Mat;
    public Material blurMat;
    public Material brightMat;
    public Material bloomMat;
    public Material edgeMat;
    public RenderTexture fullFbo;
    Texture2D test;



    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

        bright = src;

          //Graphics.Blit(src, bright, brightMat);
          //Graphics.Blit(bright, blur, blurMat);
          Graphics.Blit(fullFbo, dest, bloomMat);
    }
}
