#include "Standard.fxh"
#include "Phong_MultipleLights.fxhv"
#include "Phong_MultipleLights.fxhp"
#include "LavaAttack_Light.fxhv"
#include "LavaAttack_Light.fxhp"

//==========
// Globals
//==========

//texture
uniform extern texture gTexAnimated;

//==================
//	Texture Samples
//==================

sampler TexLava_Light = sampler_state
{
	Texture = <gTexAnimated>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
	AddressU  = wrap;
    AddressV  = wrap;
};

//========================
// VS/PS structures
//========================

/* Data from application vertex buffer */
struct appdata{
	float3 posL		: POSITION0;
	float3 normalL	: NORMAL0;
	float2 tex0		: TEXCOORD0;
};



/* data passed from vertex shader to pixel shader */
struct stratVertOut{
    float4 posH    : POSITION0;
    float2 tex0    : TEXCOORD0;
    float3 normalW : TEXCOORD1;
};

//========================
// Techniques
//========================

technique MultipleLights
{
    pass P0
    {
        vertexShader = compile vs_2_0 Phong_MultipleLights_VS();
        pixelShader  = compile ps_2_0 Phong_MultipleLights_PS();
        
        ZEnable = true;
		ZWriteEnable = true;
		
		CullMode = CCW;
    }
}
technique LavaAttack_Light
{
    pass P0
    {
        vertexShader = compile vs_2_0 LavaAttack_Light_VS();
        pixelShader  = compile ps_2_0 LavaAttack_Light_PS();
        
        ZEnable = true;
		ZWriteEnable = true;
		
		CullMode = CCW;
    }
}