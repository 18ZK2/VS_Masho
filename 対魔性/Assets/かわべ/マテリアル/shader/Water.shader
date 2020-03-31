Shader "Custom/Water"
{
    Properties
    {
		_NormalTex ("Normal Tex",2D) ="bump"{}
		_Distortion("Distortion", Range(-1,1)) = 1
		_EmitionPow("EmittionPow",Range(0,1))=1
		_ScrollSpeed("ScrollSpeed",Float)=0
		_Color("Color",color)=(0,0,0,0)
    }
    SubShader
    {
        Tags {
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
        LOD 200
		GrabPass {}
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
		sampler2D _GrabTexture;
		sampler2D _NormalTex;
		float _Distortion;
		float _EmitionPow;
		float _ScrollSpeed;
		fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos;
			float2 uv_NormalTex;
        };
 

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 grabUV = (IN.screenPos.xy / IN.screenPos.w);// * float2(1, -1) + float2(0, 1);
			fixed2 normalTex = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex)).rg;
			grabUV += normalTex * _Distortion*_SinTime;
			fixed3 grab = tex2D(_GrabTexture, grabUV).rgb;
			o.Emission = grab;
			o.Emission *= IN.screenPos.y*IN.screenPos.y;
			if (IN.screenPos.y > 0) {
				o.Emission *= _EmitionPow;
			}
			o.Albedo = _Color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
