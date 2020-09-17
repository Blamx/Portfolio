/*
	Fragment Shader

	Sets the fragment colour
*/

#version 330

in vec3 normal0;
in vec2 texCoord;
in vec3 color0;
in vec3 eyePos;

in vec3 lightDir;

uniform sampler2D tex1; //ufo tex
uniform sampler2D tex2; //ufoMask
uniform sampler2D tex3;

uniform vec3 color1;
uniform vec3 color2;
uniform vec3 color3;


uniform float cloudRamp;

void main()
{
	float shininess = 3.0; // make this a uniform
	float specPower = 25.0;
	vec3 L = lightDir;
	vec3 N = normalize(normal0);

	vec2 uvfliped = vec2(texCoord.s, 1 - texCoord.t);

	vec4 colorTint =  vec4(color1.x, color1.y, color1.z, 1.0);//touch

	// light colours
	vec4 ambient = vec4(0.1, 0.1, 0.1, 1.0);
	vec4 diffuseColour = texture2D(tex1, uvfliped);
	vec4 specularColour = vec4(0.5, 0.5, 0.5, 1.0);

	// diffuse component
	float lightIntensity = clamp(dot(normal0, L), 0.0, 1.0);
	vec4 diffuse = diffuseColour * lightIntensity;

	// specular component
	vec3 E = normalize(-eyePos);
	vec3 H = normalize(L + E);
	float ndoth = max(dot(N, H), 0.0);
	float specularCoefficient = pow(ndoth, specPower) *shininess;
	vec4 specular = specularColour * specularCoefficient;

	// combine
	vec4 color = ambient + diffuse + specular;

	//color.w = 1.0;

	vec4 mask = texture2D(tex2, uvfliped);

	vec4 mask_r = vec4(color1 * mask.r, 1);
	vec4 mask_g = vec4(color2 * mask.g, 1);
	vec4 mask_b = vec4(color3 * mask.b, 1);

	gl_FragColor = (mask_r + mask_g + mask_b) * color;
}