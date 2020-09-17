#version 330

in vec3 normal0;
in vec2 texCoord;
in vec3 color0;
in vec3 lightDir;

uniform sampler2D tex1;
uniform sampler2D tex2;
uniform float dispNormals;
uniform vec4 color1;
uniform float intents;

void main()
{
	vec4 texColor = texture(tex1, texCoord.xy)* intents + texture(tex2, texCoord.xy)/ intents;

	gl_FragColor = texColor;
}

/*
#version 400

uniform sampler2D u_tex_bright;
uniform sampler2D u_tex_scene;

// Fragment Shader Inputs
in VertexData
{
	vec3 normal;
	vec3 texCoord;
	vec4 colour;
	vec3 posEye;
} vIn;

layout(location = 0) out vec4 FragColor;


void main()
{
	vec4 texColor = texture(u_tex_bright, vIn.texCoord.xy) + texture(u_tex_scene, vIn.texCoord.xy);

	FragColor = texColor;
}*/