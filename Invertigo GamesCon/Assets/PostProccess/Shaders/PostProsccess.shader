Shader "Post/PostProsccess"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			//Vertex
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			

			sampler2D _MainTex;

			//Fragment
			fixed4 frag (v2f In) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, In.uv);
				// just invert the colors
				//col = (1 - col);
			
			
			
			/*if (col.r > 0.9f)
				col.r = 1;
			else if (col.r > 0.8f)
				col.r = 0.9;
			else if (col.r > 0.7f)
				col.r = 0.8;
			else if (col.r > 0.6f)
				col.r = 0.7;
			else if (col.r > 0.5f)
				col.r = 0.6;
			else if (col.r > 0.4f)
				col.r = 0.5;
			else if (col.r > 0.3f)
				col.r = 0.4;
			else if (col.r > 0.2f)
				col.r = 0.3;
			else if (col.r > 0.1f)
				col.r = 0.2;
			else
				col.r = 0;

			if (col.b > 0.9f)
				col.b = 1;
			else if (col.b > 0.8f)
				col.b = 0.9;
			else if (col.b > 0.7f)
				col.b = 0.8;
			else if (col.b > 0.6f)
				col.b = 0.7;
			else if (col.b > 0.5f)
				col.b = 0.6;
			else if (col.b > 0.4f)
				col.b = 0.5;
			else if (col.b > 0.3f)
				col.b = 0.4;
			else if (col.b > 0.2f)
				col.b = 0.3;
			else if (col.b > 0.1f)
				col.b = 0.2;
			else
				col.b = 0;

			if (col.g > 0.9f)
				col.g = 1;
			else if (col.g > 0.8f)
				col.g = 0.9;
			else if (col.g > 0.7f)
				col.g = 0.8;
			else if (col.g > 0.6f)
				col.g = 0.7;
			else if (col.g > 0.5f)
				col.g = 0.6;
			else if (col.g > 0.4f)
				col.g = 0.5;
			else if (col.g > 0.3f)
				col.g = 0.4;
			else if (col.g > 0.2f)
				col.g = 0.3;
			else if (col.g > 0.1f)
				col.g = 0.2;
			else
				col.g = 0;*/

			
			return col;
			}
			ENDCG
		}
	}
}
