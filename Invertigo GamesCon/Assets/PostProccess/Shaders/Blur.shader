Shader "Post/Blur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		texSize("TexSize", range(0.0001,0.01)) = 0.001
		_Range("Range", int) = 3
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float2 texelSize;
			float texSize;
			int _Range;

			fixed4 frag (v2f i) : SV_Target
			{
				texelSize = float2(texSize,texSize);

				float range = 3;

				float2 offsetCoords[3][3];
				
				for (int j = 0; j < range; j++)
				{
					for (int k = 0; k < range; k++)
					{
						offsetCoords[j][k] = float2(-texelSize.x * j, texelSize.y * k) + i.uv; // [ 0 1 2 ]
					}
				}

				/*offsetCoords[0][0] = float2(-texelSize.x,  texelSize.y) + i.uv; // [ 0 1 2 ]
				offsetCoords[0][1] = float2(0.0f        ,  texelSize.y) + i.uv; // [ 3 4 5 ]
				offsetCoords[0][2] = float2(texelSize.x,  texelSize.y) + i.uv; // [ 6 7 8 ]
								 
				offsetCoords[1][0] = float2(-texelSize.x, 0.0f) + i.uv;
				offsetCoords[1][1] = float2(0.0f        , 0.0f) + i.uv;
				offsetCoords[1][2] = float2(texelSize.x, 0.0f) + i.uv;
								 
				offsetCoords[2][0] = float2(-texelSize.x, -texelSize.y) + i.uv;
				offsetCoords[2][1] = float2(0.0f        , -texelSize.y) + i.uv;
				offsetCoords[2][2] = float2(texelSize.x, -texelSize.y) + i.uv;*/

				float gaussMatrix3x3[3][3];
				
				for (int j = 0; j < range; j++)
				{
					for (int k = 0; k < range; k++)
					{
						gaussMatrix3x3[j][k] = 1/range;
					}
				}

			  /*gaussMatrix3x3[0][0] = 0.077847;
				gaussMatrix3x3[0][1] = 0.123317;
				gaussMatrix3x3[0][2] = 0.077847;
							  
				gaussMatrix3x3[1][0] = 0.123317;
				gaussMatrix3x3[1][1] = 0.195346;
				gaussMatrix3x3[1][2] = 0.123317;
							  
				gaussMatrix3x3[2][0] = 0.077847;
				gaussMatrix3x3[2][1] = 0.123317;
				gaussMatrix3x3[2][2] = 0.077847;*/

				float4 matSum = float4(0,0,0,0);

				for (int j = 0; j < range; j++)
				{
					for (int k = 0; k < range; k++)
					{
						matSum += tex2D(_MainTex, offsetCoords[j][k]).rgba * gaussMatrix3x3[j][k];
					}
				}
				/*matSum += tex2D(_MainTex, offsetCoords[0][0]).rgba * gaussMatrix3x3[0][0];
				matSum += tex2D(_MainTex, offsetCoords[0][1]).rgba * gaussMatrix3x3[0][1];
				matSum += tex2D(_MainTex, offsetCoords[0][2]).rgba * gaussMatrix3x3[0][2];

				matSum += tex2D(_MainTex, offsetCoords[1][0]).rgba * gaussMatrix3x3[1][0];
				matSum += tex2D(_MainTex, offsetCoords[1][1]).rgba * gaussMatrix3x3[1][1];
				matSum += tex2D(_MainTex, offsetCoords[1][2]).rgba * gaussMatrix3x3[1][2];

				matSum += tex2D(_MainTex, offsetCoords[2][0]).rgba * gaussMatrix3x3[2][0];
				matSum += tex2D(_MainTex, offsetCoords[2][1]).rgba * gaussMatrix3x3[2][1];
				matSum += tex2D(_MainTex, offsetCoords[2][2]).rgba * gaussMatrix3x3[2][2];*/

				fixed4 col = float4(matSum.rgba); //every frag shader must set gl_FragColor

				return col;
			}
			ENDCG
		}
	}
}
