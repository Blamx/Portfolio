/*
	Fragment Shader

	Sets the fragment colour
*/

#version 330

in vec3 normal0;
in vec2 texCoord;
in vec4 color0;
in vec3 lightDir;

uniform sampler2D tex1;
uniform float dispNormals;
uniform vec4 color1;
uniform float modx;
uniform float mody;
uniform float alpha;


void main()
{
	vec4 color;


	vec3 L = lightDir;


		color =  texture2D(tex1, vec2(texCoord.s + modx*0.5625, texCoord.t + mody)); //vec4(color1.x, color1.y, color1.z, 1.0);
		color = color;
	

	gl_FragColor = vec4(color.x, color.y, color.z, color.a - alpha); //every frag shader must set gl_FragColor
}