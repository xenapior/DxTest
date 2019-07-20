using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Excercise01GL
{
	public class BufferObject<T>: GlResource where T:struct
	{
		private int _bufferSize;
		private int _dataSize;

		public int BufferSize => _bufferSize;
		public int DataSize => _dataSize;

		public BufferObject(T[] data, BufferStorageFlags storageFlags = BufferStorageFlags.DynamicStorageBit)
		{
			GL.CreateBuffers(1, out id);
			_dataSize = Marshal.SizeOf<T>();
			_bufferSize = _dataSize*data.Length;
			GL.NamedBufferStorage(id, _bufferSize, data, storageFlags);
		}

		public void SetData(T[] data, int offset=0)
		{
			GL.NamedBufferSubData(id,(IntPtr)offset,_dataSize*data.Length,data);
		}

		public override void Dispose()
		{
			if (id != -1)
			{
				GL.DeleteBuffer(id);
			}
		}
	}
}
