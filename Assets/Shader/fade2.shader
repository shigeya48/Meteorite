// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "uGui/fade"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Texture("Texture", 2D) = "white" {}
		_MaskScale("Mask Scale", Float) = 1
		_Emi_Value("Emi_Value", Float) = 1
		_Maskmanual("Mask manual", Float) = 0.17
		_emicolor("emi color", Color) = (1,1,1,0)
		[Toggle(_AUTOFADESWITCH_ON)] _AutoFadeSwitch("Auto Fade Switch", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TreeBillboard"  "Queue" = "Background+0" "IsEmissive" = "true"  }
		LOD 13
		Cull Back
		AlphaToMask On
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 5.0
		#pragma shader_feature _AUTOFADESWITCH_ON
		#pragma surface surf StandardSpecular keepalpha 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _emicolor;
		uniform float _Emi_Value;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;
		uniform half _MaskScale;
		uniform float _Maskmanual;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Emission = ( _emicolor * _Emi_Value ).rgb;
			o.Alpha = 1;
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float div75=256.0/float(1);
			float4 posterize75 = ( floor( tex2D( _Texture, uv_Texture ) * div75 ) / div75 );
			#ifdef _AUTOFADESWITCH_ON
				float staticSwitch141 = ( _SinTime.w * 2.0 );
			#else
				float staticSwitch141 = _MaskScale;
			#endif
			float4 temp_cast_1 = ((0.0 + (staticSwitch141 - _Maskmanual) * (1.0 - 0.0) / (1.0 - _Maskmanual))).xxxx;
			float4 color103 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			clip( ( step( posterize75 , temp_cast_1 ) * color103 ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16301
1225;1004;2539;1037;1730.569;1028.015;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;135;-1344.275,-408.3148;Float;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;134;-1394.975,-648.8148;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;-1107.655,-519.53;Half;False;Property;_MaskScale;Mask Scale;2;0;Create;True;0;0;False;0;1;1.59;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;-1084.676,-399.4151;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-978.912,-150.589;Float;False;Constant;_Float4;Float 4;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;35;-996.4732,-703.449;Float;True;Property;_Texture;Texture;1;0;Create;True;0;0;False;0;d7ea06d0b17c1f2449da59482578de9c;9637ab146f5bead46b6e32f3e9e847cd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;-820.5578,-252.0905;Float;False;Constant;_MaxOld;MaxOld;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;141;-931.978,-386.1725;Float;False;Property;_AutoFadeSwitch;Auto Fade Switch;6;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-996.3964,-249.1887;Float;False;Property;_Maskmanual;Mask manual;4;0;Create;True;0;0;False;0;0.17;1.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-824.694,-151.6484;Float;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosterizeNode;75;-609.4995,-541.2137;Float;True;1;2;1;COLOR;0,0,0,0;False;0;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;51;-574.0502,-224.2593;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;48;-265.2465,-537.1475;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;139;-46.97803,-906.1725;Float;False;Property;_emicolor;emi color;5;0;Create;True;0;0;False;0;1,1,1,0;0.5566037,0.5566037,0.5566037,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;107;35.4131,-735.0823;Float;False;Property;_Emi_Value;Emi_Value;3;0;Create;True;0;0;False;0;1;2.09;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;103;-269.0517,-256.4301;Float;False;Constant;_Color1;Color 1;3;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;3.264661,-370.7249;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;219.022,-810.1725;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;153;403.522,-734.7068;Float;False;True;7;Float;ASEMaterialInspector;13;0;StandardSpecular;uGui/fade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;TreeBillboard;;Background;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;13;;-1;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;132;0;134;4
WireConnection;132;1;135;0
WireConnection;141;1;94;0
WireConnection;141;0;132;0
WireConnection;75;1;35;0
WireConnection;51;0;141;0
WireConnection;51;1;53;0
WireConnection;51;2;52;0
WireConnection;51;3;55;0
WireConnection;51;4;54;0
WireConnection;48;0;75;0
WireConnection;48;1;51;0
WireConnection;100;0;48;0
WireConnection;100;1;103;0
WireConnection;137;0;139;0
WireConnection;137;1;107;0
WireConnection;153;2;137;0
WireConnection;153;10;100;0
ASEEND*/
//CHKSM=88D9FB438380DC999B3B1AE4679ECB520279A6B5