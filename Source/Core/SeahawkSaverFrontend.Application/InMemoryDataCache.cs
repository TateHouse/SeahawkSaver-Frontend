namespace SeahawkSaverFrontend.Application.UnitTest;
using SeahawkSaverFrontend.Application.Abstractions.Application;

public sealed class InMemoryDataCache<TData> : IDataCache<TData>
{
	public TData? Data { get; private set; }

	public void UpdateCache(TData data)
	{
		Data = data;
	}
}