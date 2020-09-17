Shader "Unlit/Grow"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)
		_Size("Size", Range(0,2)) = 1.2
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 300
		Cull Front

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag


#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _Color;
	float _Size;

	v2f vert(appdata v)
	{
		v2f o;
		v.vertex.x += v.normal.x * _Size;
		v.vertex.y += v.normal.y * _Size;
		v.vertex.z += v.normal.z * _Size;

		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = _Color;
	return col;
	}
		ENDCG
	}
	}
}
