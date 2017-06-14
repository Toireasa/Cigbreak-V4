Shader "Custom/UIMasking"
{
	Properties
	{
		_Color ("Color", COLOR) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RBG)", 2D) = "" {}
		_Mask ("Alpha Mask", 2D) = "" {}
		_Intensity("Intensity", Range(0.0, 1.0)) = 0.5
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Unlit alpha

		sampler2D _MainTex;
		sampler2D _Mask;
		float4 _Color;
		float _Intensity;

		inline fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo * _Intensity;
			c.a = s.Alpha;
			return c;
		}

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			half4 mask = tex2D(_Mask, IN.uv_MainTex);

			o.Albedo = c.rgb;
			o.Alpha = min(mask.g, _Color.a);
		}
		ENDCG
	}
}