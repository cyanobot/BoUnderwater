Shader "Shader Forge/CausticsShader" {
	Properties {
		_MainTex ("MainTex", 2D) = "bump" {}
		_Color ("Color", Vector) = (0,0.9817288,1,1)
		[HDR] _SceneColorMult ("SceneColorMult", Vector) = (0.5,0.5,0.5,1)
		_LayerOneScrollSpeedX ("LayerOneScrollSpeedX", Float) = 0.1
		_LayerOneScrollSpeedY ("LayerOneScrollSpeedY", Float) = 0.1
		_LayerOneZoomScale ("LayerOneZoomScale", Float) = 1
		_LayerTwo ("LayerTwo", 2D) = "black" {}
		_ColorTwo ("ColorTwo", Vector) = (0.5,0.5,0.5,1)
		_LayerTwoZoomScale ("LayerTwoZoomScale", Float) = 0.1
		_LayerTwoSpeedX ("LayerTwoSpeedX", Float) = 1
		_LayerTwoSpeedY ("LayerTwoSpeedY", Float) = 1
		_VoronoiCellDensity ("VoronoiCellDensity", Float) = 10
		_VoronoiSpeed ("VoronoiSpeed", Float) = 0.01
		_VoronoiColor1 ("VoronoiColor1", Vector) = (1,1,1,1)
		_VoronoiColor2 ("VoronoiColor2", Vector) = (1,1,1,1)
		_VoronoiMax ("VoronoiMax", Float) = 0.5
		[MaterialToggle] _EnableDistortion ("EnableDistortion", Float) = 1
		_DistortionStrR ("DistortionStrR", Float) = 0.01
		_DistortionStrG ("DistortionStrG", Float) = 0.01
		_DistortMap ("DistortMap", 2D) = "bump" {}
		_DistortionSpeedX ("DistortionSpeedX", Float) = 1
		_DistortionSpeedY ("DistortionSpeedY", Float) = 0
		_Opacity ("Opacity", Float) = 0.5
		_DistortionScale ("DistortionScale", Float) = 1
		_GlobalScale ("GlobalScale", Float) = 1
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Stencil ("Stencil ID", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilComp ("Stencil Comparison", Float) = 8
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilOpFail ("Stencil Fail Operation", Float) = 0
		_StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;
			float4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy) * _Color;
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}