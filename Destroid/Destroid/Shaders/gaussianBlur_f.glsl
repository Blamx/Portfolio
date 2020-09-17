#version 330

in vec3 normal0;
in vec2 texCoord;
in vec3 color0;
in vec3 lightDir;

uniform sampler2D tex1;
uniform float dispNormals;
uniform vec4 color1;
uniform vec4 texelSize;

void main()
{
	vec2 offsetCoords[9];
		offsetCoords[0] = vec2(-texelSize.x,  texelSize.y) + texCoord.xy; // [ 0 1 2 ]
		offsetCoords[1] = vec2(0.0f        ,  texelSize.y) + texCoord.xy; // [ 3 4 5 ]
		offsetCoords[2] = vec2( texelSize.x,  texelSize.y) + texCoord.xy; // [ 6 7 8 ]
	
		offsetCoords[3] = vec2(-texelSize.x, 0.0f        ) + texCoord.xy;
		offsetCoords[4] = vec2(0.0f        , 0.0f        ) + texCoord.xy;
		offsetCoords[5] = vec2( texelSize.x, 0.0f        ) + texCoord.xy;
	
		offsetCoords[6] = vec2(-texelSize.x, -texelSize.y) + texCoord.xy;
		offsetCoords[7] = vec2(0.0f        , -texelSize.y) + texCoord.xy;
		offsetCoords[8] = vec2( texelSize.x, -texelSize.y) + texCoord.xy;
	
	float gaussMatrix3x3[9];
		gaussMatrix3x3[0] = 0.095332f;	
		gaussMatrix3x3[1] = 0.118095f;	
		gaussMatrix3x3[2] = 0.095332f;
	
		gaussMatrix3x3[3] = 0.118095f;	
		gaussMatrix3x3[4] = 0.146293f;	
		gaussMatrix3x3[5] = 0.118095f;
	
		gaussMatrix3x3[6] = 0.095332f;	
		gaussMatrix3x3[7] = 0.118095f;	
		gaussMatrix3x3[8] = 0.095332f;
	
	vec4 matSum = vec4(0.0f);
		matSum += texture(tex1, offsetCoords[0]).rgba * gaussMatrix3x3[0];
		matSum += texture(tex1, offsetCoords[1]).rgba * gaussMatrix3x3[1];
		matSum += texture(tex1, offsetCoords[2]).rgba * gaussMatrix3x3[2];
	
		matSum += texture(tex1, offsetCoords[3]).rgba * gaussMatrix3x3[3];
		matSum += texture(tex1, offsetCoords[4]).rgba * gaussMatrix3x3[4];
		matSum += texture(tex1, offsetCoords[5]).rgba * gaussMatrix3x3[5];
	
		matSum += texture(tex1, offsetCoords[6]).rgba * gaussMatrix3x3[6];
		matSum += texture(tex1, offsetCoords[7]).rgba * gaussMatrix3x3[7];
		matSum += texture(tex1, offsetCoords[8]).rgba * gaussMatrix3x3[8];
	
	gl_FragColor = vec4(matSum.rgba); //every frag shader must set gl_FragColor
}

