Shader "Custom/DisabledCoins"
{
	Properties
	{
		_MainTex ("Base (RBG)", 2D) = "" {}
		_Intensity ("Intensity", Range(0, 1)) = 0.0
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Unlit

		sampler2D _MainTex;
		float _Intensity;

		inline fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c = fixed4(1,1,1,1);
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

			o.Albedo = c.rgb;
		}
		ENDCG
	}
}