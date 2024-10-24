namespace SeahawkSaverFrontend.Application.Abstractions.Application;
public abstract class DataCache<TData>
{
	public TData? Data { get; private set; }

	public void UpdateCache(TData data)
	{
		Data = data;
	}
}