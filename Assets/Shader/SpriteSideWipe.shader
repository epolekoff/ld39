// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteSideWipe"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Progress("Progress", Range(0, 1)) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"PreviewType" = "Plane"
	}
		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha

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
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	float _Progress;

	float4 frag(v2f i) : SV_Target
	{
		float4 color = tex2D(_MainTex, i.uv);
		color.a = 1;// 1 - clamp(i.uv.x - _Progress, 0, 1);
		return color;
	}
		ENDCG
	}
	}
}