Shader "Hidden/Rusty"
{
	Properties{
		_NoisePow("Noise Power",Range(0.5,1)) = 0
		_MainTex("Sprite Texture", 2D) = "white" {}
		_RustyColor("RustyColor",Color) = (1,1,1,1)
		_RustyColor2("RustyColor2",Color)=(1,1,1,1)
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

			float _NoisePow;
			sampler2D _MainTex;
			float4 _RustyColor;
			float4 _RustyColor2;

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
			float random(fixed2 p) {
				return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453)*_NoisePow;
			}
			float3 random_Time(fixed2 p) {
				float3 col = 0.5 + 0.5 * cos(_Time - p.xyx);
				return col;
			}
			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.sv_pos = UnityObjectToClipPos(input.pos);
				output.uv = input.uv;
				output.color = input.color;
				return output;
			}

			float4 frag(VertexOutput output) :SV_Target{
				float2 tex = output.uv;
				float4 c = tex2D(_MainTex, output.uv) * output.color;
				c.rgb *= c.a;
				float r = random(tex);
				if (r > 0.5) {
					c.rgb *= _RustyColor;
					c.rgb *= r;
				}
				return c;
			}
            ENDCG
        }
    }
}
