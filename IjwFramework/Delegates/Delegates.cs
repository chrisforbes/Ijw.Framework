namespace IjwFramework.Delegates
{
	public delegate T Provider<T>();
	public delegate T Provider<T, U>(U u);
	public delegate T Provider<T, U, V>(U u, V v);
	public delegate void Action();
}