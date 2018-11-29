Shader "ShadowEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		ModelTex ("Texture", 2D) = "white" {}
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
			sampler2D ModelTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				fixed4 shadowColor = tex2D(ModelTex, i.uv);
				
				float coefficient = length(i.uv - float2(0.5, 0.5));
				float maxSize = length(float2(0.5, 0.5));
				fixed4 resultColor = color;
				if (shadowColor.r > 0.05 || shadowColor.g > 0.05 || shadowColor.b > 0.05)
					resultColor = 0;
				else
					resultColor *= coefficient/maxSize;
				// just invert the colors
				//col.rgb = 1 - col.rgb;
				return resultColor;
			}
			ENDCG
		}
	}
}
