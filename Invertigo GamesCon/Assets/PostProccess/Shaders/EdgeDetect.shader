Shader "Post/EdgeDetect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		texelSizes("TexleSize", Range(0.00001, 0.01)) = 0.0001
		rangeMod("RangeMod", Range(1, 10)) = 1
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	float2 texelSize;
	float texelSizes;
	float rangeMod;
	fixed4 frag(v2f i) : SV_Target
	{

	texelSize = float2(texelSizes,texelSizes);
		float2 offsetCoords[9];
	offsetCoords[0] = float2(-texelSize.x,  texelSize.y) + i.uv; // [ 0 1 2 ]
	offsetCoords[1] = float2(0.0f        ,  texelSize.y) + i.uv; // [ 3 4 5 ]
	offsetCoords[2] = float2(texelSize.x,  texelSize.y) + i.uv; // [ 6 7 8 ]

	offsetCoords[3] = float2(-texelSize.x, 0.0f) + i.uv;
	offsetCoords[4] = float2(0.0f        , 0.0f) + i.uv;
	offsetCoords[5] = float2(texelSize.x, 0.0f) + i.uv;

	offsetCoords[6] = float2(-texelSize.x, -texelSize.y) + i.uv;
	offsetCoords[7] = float2(0.0f        , -texelSize.y) + i.uv;
	offsetCoords[8] = float2(texelSize.x, -texelSize.y) + i.uv;

	float gaussMatrix3x3[9];
	gaussMatrix3x3[0] = -1 * rangeMod;
	gaussMatrix3x3[1] = -1 * rangeMod;
	gaussMatrix3x3[2] = -1 * rangeMod;

	gaussMatrix3x3[3] = -1 * rangeMod;
	gaussMatrix3x3[4] =  8 * rangeMod;
	gaussMatrix3x3[5] = -1 * rangeMod;

	gaussMatrix3x3[6] = -1 * rangeMod;
	gaussMatrix3x3[7] = -1 * rangeMod;
	gaussMatrix3x3[8] = -1 * rangeMod;

	float4 matSum = float4(0,0,0,0);
	matSum += tex2D(_MainTex, offsetCoords[0]).rgba * gaussMatrix3x3[0];
	matSum += tex2D(_MainTex, offsetCoords[1]).rgba * gaussMatrix3x3[1];
	matSum += tex2D(_MainTex, offsetCoords[2]).rgba * gaussMatrix3x3[2];

	matSum += tex2D(_MainTex, offsetCoords[3]).rgba * gaussMatrix3x3[3];
	matSum += tex2D(_MainTex, offsetCoords[4]).rgba * gaussMatrix3x3[4];
	matSum += tex2D(_MainTex, offsetCoords[5]).rgba * gaussMatrix3x3[5];

	matSum += tex2D(_MainTex, offsetCoords[6]).rgba * gaussMatrix3x3[6];
	matSum += tex2D(_MainTex, offsetCoords[7]).rgba * gaussMatrix3x3[7];
	matSum += tex2D(_MainTex, offsetCoords[8]).rgba * gaussMatrix3x3[8];

	fixed4 col = float4(matSum.rgba); //every frag shader must set gl_FragColor

	if (col.r > 0.5 || col.b > 0.5 || col.g > 0.5)
		col.rgb = float3(1, 1, 1);

	return col;
	}
		ENDCG
	}
	}
}
