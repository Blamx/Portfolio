using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bloom : MonoBehaviour {


    public RenderTexture bright;
    public RenderTexture blur;
    public RenderTexture edge;
    public RenderTexture holder;
    public Material Mat;
    public Material blurMat;
    public Material brightMat;
    public Material bloomMat;
    public Material edgeMat;
    public RenderTexture fullFbo;
    Texture2D test;

    public bool bloom = true;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

        bright = src;
        blur = src;

        //bloom
        if (bloom)
        {
            Graphics.Blit(fullFbo, bright, brightMat);
            Graphics.Blit(bright, dest, blurMat);

        }

        if (!bloom)
        {
            Graphics.Blit(fullFbo, bright, Mat);
            Graphics.Blit(bright, edge, edgeMat);
            Graphics.Blit(edge, holder, blurMat);
            Graphics.Blit(holder, dest, brightMat);
            //Graphics.Blit(blur, dest, Mat);
        }
    }
}