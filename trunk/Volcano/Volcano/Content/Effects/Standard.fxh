/* Standard.fxh
 * 
 * Common values and methods for use in shader files.
 *
 */

#ifndef _STANDARD_FXH_
#define _STANDARD_FXH_

//-------------------------
// Numerical Constants
//-------------------------
#define PI 3.1415926535f

//-------------------------
// Space Transformation Matricies
//-------------------------
uniform extern float4x4 gWorld;
uniform extern float4x4 gWIT;
uniform extern float4x4 gWInv;
uniform extern float4x4 gWVP;


//-------------------------
// World's Eye and Light
//-------------------------
uniform extern float3 gLightVecW;
uniform extern float3 gEyePosW;

//-------------------------
// Lighting
//-------------------------
uniform extern float4 gDiffuseMtrl;
uniform extern float4 gDiffuseLight;
uniform extern float4 gAmbientMtrl;
uniform extern float4 gAmbientLight;
uniform extern float4 gSpecularMtrl;
uniform extern float4 gSpecularLight;
uniform extern float  gSpecularPower;

//-------------------------
// Time
//-------------------------
uniform extern float  gTime;

//-------------------------
// Volcano stuff
//-------------------------
uniform extern float  gIsTiki;
uniform extern float  gIsHut;


//-------------------------
// Colorshift Values
//-------------------------
uniform extern bool	  gColorShift;

//-------------------------
// Blob Values
//-------------------------
uniform extern bool	  gBlob;
uniform extern float  gBlobStrecth;

//-------------------------
// Rotate Values
//-------------------------
uniform extern bool	  gRotate;
uniform extern float  gRotTorque;
uniform extern int	  gRotAxis;
uniform extern float  gRotSpeed;

//==================
//	Textures
//==================

uniform extern texture gTex;

//==================
//	Texture Samples
//==================

sampler TexS = sampler_state
{
	Texture = <gTex>;
	MinFilter = linear;
	MagFilter = linear;
	MipFilter = linear;
	AddressU  = wrap;
    AddressV  = wrap;
};

//-------------------------
// Methods
//-------------------------
float4 Rotate(float4 inPos,float time, int axis){
	float4 outPos = inPos;
	time *= gRotSpeed;
	float sin,cos;
	
	sincos(time,sin,cos);
	
	if(axis == 'x'){
		float3x3 xRot;
		xRot[0] = float3(1.0f,0.0f,0.0f);
		xRot[1] = float3(0.0f,cos,sin);
		xRot[2] = float3(0.0f,-sin,cos);
		outPos.x = (xRot[0][0]*inPos.x)+(xRot[1][0]*inPos.y)+(xRot[2][0]*inPos.z);
		outPos.y = (xRot[0][1]*inPos.x)+(xRot[1][1]*inPos.y)+(xRot[2][1]*inPos.z);
		outPos.z = (xRot[0][2]*inPos.x)+(xRot[1][2]*inPos.y)+(xRot[2][2]*inPos.z);	
		return outPos;
	}
	else if(axis == 'y'){
		float3x3 yRot;
		yRot[0] = float3(cos,0.0f,-sin);
		yRot[1] = float3(0.0f,1.0f,0.0f);
		yRot[2] = float3(sin,0.0f,cos);
		outPos.x = (yRot[0][0]*inPos.x)+(yRot[1][0]*inPos.y)+(yRot[2][0]*inPos.z);
		outPos.y = (yRot[0][1]*inPos.x)+(yRot[1][1]*inPos.y)+(yRot[2][1]*inPos.z);
		outPos.z = (yRot[0][2]*inPos.x)+(yRot[1][2]*inPos.y)+(yRot[2][2]*inPos.z);	
		return outPos;
	}
	else if(axis == 'z'){
		float3x3 zRot;
		zRot[0] = float3(cos,sin,0.0f);
		zRot[1] = float3(-sin,cos,0.0f);
		zRot[2] = float3(0.0f,0.0f,1.0f);
		outPos.x = (zRot[0][0]*inPos.x)+(zRot[1][0]*inPos.y)+(zRot[2][0]*inPos.z);
		outPos.y = (zRot[0][1]*inPos.x)+(zRot[1][1]*inPos.y)+(zRot[2][1]*inPos.z);
		outPos.z = (zRot[0][2]*inPos.x)+(zRot[1][2]*inPos.y)+(zRot[2][2]*inPos.z);	
		return outPos;
	}
	else{
		return inPos;
	}
}


float3 Blob(float3 position, float3 normal, float stretch){
	float angle=(gTime%360)*2;
    float freqx = 1.0f+sin(gTime)*4.0f;
    float freqy = 1.0f+sin(gTime*1.3f)*4.0f;
    float freqz = 1.0f+sin(gTime*1.1f)*4.0f;
    float amp = 1.0f+sin(gTime*1.4)*stretch;
   
    float f = sin(normal.x*freqx + gTime) * sin(normal.y*freqy + gTime) * sin(normal.z*freqz + gTime);
    position.z += normal.z * amp * f;
    position.x += normal.x * amp * f;
    position.y += normal.y * amp * f;
	
	return position;
}

float ClampedSin(float ceil, float floor, float inNum, float scale){
	float range = sin(gTime) * scale;
	if(range >= ceil){
		inNum = ceil;
	}
	else if(range <= floor){
		inNum = floor;
	}
	else{
		inNum = range;
	}
	return inNum;
}

float3 ColorShift(){
	float3 outColor;
	outColor.r = ClampedSin(1.0f,0.1f,gDiffuseMtrl.r,0.0f);
    outColor.g = ClampedSin(1.0f,0.1f,gDiffuseMtrl.g,0.0f);
    outColor.b = ClampedSin(1.0f,0.1f,gDiffuseMtrl.b,0.0f);
    return outColor;
}

#endif //#ifndef _STANDARD_FXH_