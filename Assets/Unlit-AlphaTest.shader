// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Transparent Cutout" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_Dist ("Distance", Float) = 0
}
SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 100

	Lighting Off
    Cull Off

	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;
			fixed _Dist;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 _FogColor = {31f/255f,31f/255f,42f/255f,1f};
				float4 _LowColor = {168f/255f,34f/255f,30f/255f,1f};
				float4 _HighColor = {73f/255f,47f/255f,67f/255f,1f};
				float4 _LitColor = {234f/255f,126f/255f,65f/255f,1f};
				float4 z = {0f,0f,0f,1f};
				
				fixed4 col = tex2D(_MainTex, i.texcoord);
				float fogFactor = (30 - _Dist)/(30 - 1);
				fogFactor = clamp( fogFactor, 0.0, 1.0 );
				clip(col.a - _Cutoff);
				float4 h = mix(z,_HighColor*col.r,fogFactor);
				float4 g = _LitColor*col.g;
				fogFactor = (120 - _Dist)/(120 - 30);
				fogFactor = clamp( fogFactor, 0.0, 1.0 );
				float4 l = mix(z,_LowColor*col.b,fogFactor);
				float c = 0f;
				if(_Cutoff>0.05f){
					c = (float) _Cutoff/0.8;
					clip(-col.g);
				}


        //if you inverse color in glsl mix function you have to
        //put 1.0 - fogFactor
    			return mix(clamp(_FogColor+l+h+g,0.0,1.0),_FogColor,c);
			}
		ENDCG
	}
}

}