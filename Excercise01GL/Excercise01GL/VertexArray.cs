using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Excercise01GL
{
	public class VertexArray:GlResource
	{
		public VertexArray()
		{
			GL.CreateVertexArrays(1,out id);
		}

		public void AddVertexBufferAtLoc(int bufferObject, int location, int vecN, VertexAttribPointerType dataType, int offset=0, int stride=0)
		{
			GL.BindVertexArray(id);
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferObject);
			GL.VertexAttribPointer(location, vecN, dataType, false, stride, offset);
			GL.EnableVertexAttribArray(location);
		}

/*		public void AddSlot(int attribLoc, int attribSize, VertexAttribType type, int offset=0, int bindingLoc=0)
		{
			if (bindingLoc < bufferBindingPos)
			{
				throw new ArgumentOutOfRangeException("Binding location coincident!");
			}
			GL.BindVertexArray(id);
			GL.EnableVertexArrayAttrib(id, attribLoc);
			GL.VertexArrayAttribFormat(id, attribLoc, attribSize, type, false, offset);
			GL.VertexArrayAttribBinding(id, attribLoc, bindingLoc);
		}

		public void BindVertexBuffer<T>(BufferObject<T> bufferObject, int bindingLoc=0, int stride=0) where T : struct
		{
			if (stride == 0)
			{
				stride = bufferObject.DataSize*;
			}
			GL.VertexArrayVertexBuffer(id, bindingLoc, bufferObject, IntPtr.Zero, stride);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="bufferObject"></param>
		/// <param name="location"></param>
		/// <param name="vecN">Must be 1,2,3 or 4</param>
		/// <param name="type"></param>
		/// <param name="offset"></param>
		/// <param name="stride">Set to 0 if consecutive</param>
		public void AddVertexBufferAtLoc<T>(BufferObject<T> bufferObject, int location, int vecN, VertexAttribType type, int offset=0, int stride=0) where T : struct
		{
			AddSlot(location, vecN, type, offset, bufferBindingPos);
			BindVertexBuffer(bufferObject, bufferBindingPos, stride);
			bufferBindingPos++;
		}
		*/
		public override void Dispose()
		{
			if (id != -1)
			{
				GL.DeleteBuffer(id);
			}
		}
	}
}
