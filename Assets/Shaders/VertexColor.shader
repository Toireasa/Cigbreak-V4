// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Debug/Vertex color" {
SubShader {
    Pass {
        Fog { Mode Off }
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
 
// vertex input: position, color
struct appdata {
    float4 vertex : POSITION;
    float4 color : COLOR;
};
 
struct v2f {
    float4 pos : POSITION;
    float4 color : COLOR;
};
v2f vert (appdata v) {
    v2f o;
    o.pos = UnityObjectToClipPos( v.vertex );
    o.color = v.color;
    return o;
}
float4 frag(v2f input) : COLOR
{
	return input.color;
}
ENDCG
    }
}
}