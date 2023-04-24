namespace EndSickness.Services
{
    public interface IEndCrudClient<TResponse>
    {
        Task<TResponse> Send(HttpRequestMessage request);
    }
}