Shader "Custom/Smoke"
{
	Properties
	{
		_MainTex ("Base (RBG)", 2D) = "" {}
		_Color ("Color", COLOR) = (1, 1, 1, 0.5)
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Unlit alpha

		sampler2D _MainTex;
		float4 _Color;

		inline fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c = fixed4(1,1,1,1);
			c.rgb = c * s.Albedo;
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
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}