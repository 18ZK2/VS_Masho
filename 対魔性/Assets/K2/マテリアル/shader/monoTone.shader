Shader "Custom/monoTone" {
    Properties {
        _MainTex("MainTex", 2D) = ""{}
		_Radius("Radius",Range(0,2)) = 2
	}

		SubShader{
			Pass {
				CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert_img
				#pragma fragment frag

				float _Radius;
				sampler2D _MainTex;

				fixed4 frag(v2f_img i) : COLOR{
					fixed4 c = tex2D(_MainTex, i.uv);
					if(distance(i.uv,fixed2(0.5,0.5))>_Radius){
						float gray = 0;//c.r * 0.3 + c.g * 0.6 + c.b * 0.1;
						c.rgb = fixed3(gray, gray, gray);
					}
					return c;
				}

            ENDCG
        }
    }
}