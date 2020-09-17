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

uniform sampler2D tex1;
uniform sampler2D tex2;
uniform sampler2D tex3;
uniform vec3 color1;
uniform mat4 mvm; // modelview matrix

layout(location = 7) out vec4 FragColor;
layout(location = 6) out vec4 FragNormal;


void main()
{


	vec3 L = normalize(lightDir.xyz - (-eyePos).xzy);
	//vec3 N = normalize(normal); // Normal from VBO (obj file)

	vec3 N = texture(tex2, texCoord.xy).xyz;
	N = N * 2.0 - 1.0;

	N = (mvm * vec4(N, 0.0)).xyz;
	N = normalize(N);

	//float diffuse = max(0.0, dot(N, L));

	vec3 diffuseColor = texture(tex1, texCoord.xy).xyz*(color1)*1.5;// +vec3(0.2);
	float diffuseInts = 1.0;
	float ndotl = max(0.0, dot(N, L));
	vec3 diffuse = ndotl * diffuseColor * diffuseInts;

	//specular term
	vec3 specColor = vec3(1.0);
	float specInts = 1.75;

	vec3 V = -normalize(eyePos);
	vec3 H = normalize(L + V);
	float ndoth = max(0.0, dot(N, H));
	float specPower = 100.0;
	vec3 specular = pow(ndoth, specPower)* specInts * specColor;

	// Write to color fbo target
	FragColor = vec4(specular, 1.0) + vec4(diffuse, 1.0);
	//FragColor = vec4(vec3(0.5, 0.5, 0.5) * (diffuse * 0.8f) + u_colour.rgb, 1.0);

	// Write to normal fbo target
	FragNormal = vec4(N + 1, 1.0);
}