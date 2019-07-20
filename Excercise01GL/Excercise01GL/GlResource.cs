namespace Excercise01GL
{
	public abstract class GlResource
	{
		protected int id = -1;
		public abstract void Dispose();
	    public static implicit operator int(GlResource resource) => resource.id;
	}
}
