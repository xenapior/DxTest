using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Excercise01GL.Properties;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Excercise01GL
{
	class MainGLWindow : GameWindow
	{
		private readonly List<GlResource> autoCollector=new List<GlResource>();
		private GlProgram glslProgram;
		private VertexArray vao;
		private BufferObject<float> vbo1,vbo2;

		public MainGLWindow(int width = 800, int height = 600) : base(width, height, GraphicsMode.Default, "OpenGL")
		{
			Debug.WriteLine(GL.GetString(StringName.Version));
			int majorVer = GL.GetInteger(GetPName.MajorVersion);
			int minorVer = GL.GetInteger(GetPName.MinorVersion);
			if (majorVer < 4)
			{
				throw new NotSupportedException("OpenGL version must >= 4.5");
			}
			if (minorVer < 5)
			{
				throw new NotSupportedException("OpenGL version must >= 4.5");
			}

			string vs = Encoding.UTF8.GetString(Resources.SimpleVert);
			string fs = Encoding.UTF8.GetString(Resources.SimpleFrag);
			glslProgram = new GlProgram(new[] {vs, fs}, new[] {ShaderType.VertexShader, ShaderType.FragmentShader});
			autoCollector.Add(glslProgram);

			Load += MainGLWindow_Load;
			RenderFrame += MainGLWindow_RenderFrame;
			Unload += MainGLWindow_Unload;
		}

		private void MainGLWindow_Unload(object sender, EventArgs e)
		{
			GL.BindVertexArray(0);
			GL.UseProgram(0);
			foreach (var glResource in autoCollector)
			{
				glResource.Dispose();
			}
		}

		private void MainGLWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
			GL.BindVertexArray(vao);
			GL.DrawArrays(PrimitiveType.Triangles,0,6);
			SwapBuffers();
		}

		private void MainGLWindow_Load(object sender, EventArgs e)
		{
			float[] vt =
			{
				0, 0, 0, 1f, 0, 1f, 1f, 0.5f, 1f,
				0, 0, 1f, 0.5f, 0, 0, 0.5f, 1f, 0
			};
			float[] vtc =
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 1f, 0, 0, 1f, 0, 0, 0.5f
			};
			vao=new VertexArray();
			autoCollector.Add(vao);
			vbo1 = new BufferObject<float>(vt);
			autoCollector.Add(vbo1);
			vao.AddVertexBufferAtLoc(vbo1,0,3,VertexAttribPointerType.Float);
			vbo2 = new BufferObject<float>(vtc);
			autoCollector.Add(vbo2);
			vao.AddVertexBufferAtLoc(vbo2,1,3,VertexAttribPointerType.Float);
			GL.UseProgram(glslProgram);
			GL.ClearColor(Color.DeepSkyBlue);
			GL.Enable(EnableCap.DepthTest);
			GL.ClearDepth(1f);
			GL.Viewport(0,0,ClientSize.Width,ClientSize.Height);
		}
	}
}
