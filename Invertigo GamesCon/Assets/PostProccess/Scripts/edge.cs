using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edge : MonoBehaviour {

    public RenderTexture bright;
    public RenderTexture blur;
    public RenderTexture edgeRT;
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
        blur = src;

       // Graphics.Blit(fullFbo, bright, Mat);
       // Graphics.Blit(bright, dest, edgeMat);
        //Graphics.Blit(blur, dest, Mat);
    }
}
