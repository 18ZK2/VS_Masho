Shader "Hidden/Rusty"
{
	Properties{
			_MainTex("SpriteTexture",2D) = "white"{}
			_RustyColor("RustyColor",Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags{
				"Queue" = "Transparent"
		}
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		ZWrite Off
			Blend One OneMinusSrcAlpha //乗算済みアルファ

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _RustyColor;

			struct VertexInput {
				float4 pos:		POSITION;
				float4 color:	COLOR;
				float2 uv:		TEXCOORD0;
			};

			struct VertexOutput {
				float4 sv_pos:	SV_POSITION;
				float4 color:	COLOR;
				float2 uv:		TEXCOORD0;
			};

			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.sv_pos = UnityObjectToClipPos(input.pos);
				output.uv = input.uv;
				output.color = input.color * _RustyColor;
				return output;
			}

			float4 frag (VertexOutput output) :SV_Target{

				float4 c = tex2D(_MainTex, output.uv) * output.color;
				c.rgb *= c.a;
				return c;
			}
            ENDCG
        }
    }
}
