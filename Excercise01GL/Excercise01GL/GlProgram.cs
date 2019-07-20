using System;
using OpenTK.Graphics.OpenGL4;

namespace Excercise01GL
{
	public class GlProgram : GlResource
	{
		public GlProgram(string[] shaderSource, ShaderType[] shaderTypes)
		{
			int nShaderStages = shaderSource.Length;
			if (nShaderStages != shaderTypes.Length)
			{
				throw new ArgumentException("nShaderSource.Length!=nShaderType.Length");
			}

			id = GL.CreateProgram();

			int[] shaders=new int[nShaderStages];
			for (int i = 0; i < nShaderStages; i++)
			{
				int compileStatus;
				shaders[i] = GL.CreateShader(shaderTypes[i]);
				GL.ShaderSource(shaders[i], shaderSource[i]);
				GL.CompileShader(shaders[i]);
				GL.GetShader(shaders[i], ShaderParameter.CompileStatus, out compileStatus);
				if (compileStatus == 0)
				{
					string log=GL.GetShaderInfoLog(shaders[i]);
					GL.DeleteProgram(id);
					throw new ArgumentException(shaderTypes[i]+" compile error: "+log);
				}
				GL.AttachShader(id,shaders[i]);
			}

			GL.LinkProgram(id);
			int linkStatus;
			GL.GetProgram(id, GetProgramParameterName.LinkStatus, out linkStatus);
			if (linkStatus == 0)
			{
				string log = GL.GetProgramInfoLog(id);
				GL.DeleteProgram(id);
				throw new ArgumentException("GLSL program linking error: "+log);
			}

			for (int i = 0; i < nShaderStages; i++)
			{
				GL.DetachShader(id, shaders[i]);
				GL.DeleteShader(shaders[i]);
			}
		}

		public override void Dispose()
		{
			if (id != -1)
			{
				GL.DeleteProgram(id);
				id = -1;
			}
		}
	}
}
