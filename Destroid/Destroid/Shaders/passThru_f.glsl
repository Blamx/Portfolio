/*
	Fragment Shader

	Sets the fragment colour
*/

#version 330

in vec3 normal0;
in vec2 texCoord;
in vec3 color0;
in vec3 lightDir;

uniform sampler2D tex1;
uniform float dispNormals;
uniform vec4 color1;

void main()
{
	vec4 color;
	vec4 colorTint;

	vec3 L = lightDir;

		color =  texture2D(tex1, vec2(texCoord.s, 1 - texCoord.t)); //vec4(color1.x, color1.y, color1.z, 1.0);
		colorTint =  vec4(color1.x, color1.y, color1.z, color1.a);
		color = color * colorTint;

	gl_FragColor = color; //every frag shader must set gl_FragColor
}