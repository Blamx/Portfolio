#version 330

in vec3 normal0;
in vec2 texCoord;
in vec3 color0;
in vec3 lightDir;

uniform sampler2D tex1;
uniform float dispNormals;
uniform vec4 color1;
uniform float bloom;

void main()
{
	vec4 color;


	vec3 L = lightDir;

	color =  texture2D(tex1, vec2(texCoord.s, texCoord.t)); //vec4(color1.x, color1.y, color1.z, 1.0);


	float bloomThresh = bloom;
	color = (color - bloomThresh) / (1.0 - bloomThresh);

	gl_FragColor = color; //every frag shader must set gl_FragColor
}