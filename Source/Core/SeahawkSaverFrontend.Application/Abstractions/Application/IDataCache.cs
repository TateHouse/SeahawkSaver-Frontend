namespace SeahawkSaverFrontend.Application.Abstractions.Application;
public interface IDataCache<TData>
{
	public TData? Data { get; }

	public void UpdateCache(TData data);
}