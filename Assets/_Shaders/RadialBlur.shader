Shader "RadialBlur"
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
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			float4 frag (v2f_img img) : COLOR
			{
				float blurCoefficient = length(img.uv - float2(0.5, 0.5));
				float4 sumColor = float4(0.0, 0.0, 0.0, 0.0);
				for (int i = -1; i <= 1; i++)
					for (int j = -1; j <= 1; j++)
						sumColor += tex2D(_MainTex, float2(img.uv.x + i*0.01, img.uv.y + j*0.01));
				blurCoefficient = 1.0 - blurCoefficient;
				float4 col = sumColor*blurCoefficient/9.0;
				return col;
			}
			ENDCG
		}
	}
}
