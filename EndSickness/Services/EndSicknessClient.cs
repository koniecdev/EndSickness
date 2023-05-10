using EndSickness.Exceptions;
using EndSickness.Exceptions.ApplicationExceptions;
using EndSickness.Exceptions.Interfaces;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetDosageById;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EndSickness.Services;
public class EndSicknessClient : IEndSicknessClient
{
    private const string _mediaType = "application/json";
    private readonly IHttpContextAccessor _accessor;
    private readonly HttpClient _client;

    public EndSicknessClient(IHttpContextAccessor accessor, IHttpClientFactory clientFactory)
    {
        _accessor = accessor;
        _client = clientFactory.CreateClient("EndSickness");
    }

    public async Task<int> CreateMedicine(CreateMedicineCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .PostAsync($"{_client.BaseAddress}v1/medicines", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, _mediaType));
        var stringResult = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<int>(stringResult);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResult));
        }
    }
    public async Task UpdateMedicine(UpdateMedicineCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .PatchAsync($"{_client.BaseAddress}v1/medicines", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, _mediaType));
        if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(await result.Content.ReadAsStringAsync()));
        }
    }
    public async Task DeleteMedicine(DeleteMedicineCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .DeleteAsync($"{_client.BaseAddress}v1/medicines/{command.Id}");
        if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(await result.Content.ReadAsStringAsync()));
        }
    }
    public async Task<GetMedicinesVm> GetAllMedicines()
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicines");
        var stringResult = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetMedicinesVm>(stringResult);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResult));
        }
    }
    public async Task<GetMedicineByIdVm> GetMedicineById(GetMedicineByIdQuery query)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicines/{query.Id}");
        var stringResults = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetMedicineByIdVm>(stringResults);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResults));
        }
    }
    public async Task<GetDosagesVm> GetDosages()
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicines/dosages");
        var stringResults = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetDosagesVm>(stringResults);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResults));
        }
    }
    public async Task<GetDosageByIdVm> GetDosageById(GetDosageByIdQuery query)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicines/{query.MedicineId}/dosages");
        var stringResults = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetDosageByIdVm>(stringResults);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResults));
        }
    }
    public async Task<int> CreateMedicineLog(CreateMedicineLogCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .PostAsync($"{_client.BaseAddress}v1/medicine-logs", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, _mediaType));
        var stringResult = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<int>(stringResult);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResult));
        }
    }
    public async Task UpdateMedicineLog(UpdateMedicineLogCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .PatchAsync($"{_client.BaseAddress}v1/medicine-logs", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, _mediaType));
        if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(await result.Content.ReadAsStringAsync()));
        }
    }
    public async Task DeleteMedicineLog(DeleteMedicineLogCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .DeleteAsync($"{_client.BaseAddress}v1/medicine-logs/{command.Id}");
        if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(await result.Content.ReadAsStringAsync()));
        }
    }
    public async Task<GetMedicineLogsVm> GetAllMedicineLogs()
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicine-logs");
        var stringResult = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetMedicineLogsVm>(stringResult);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResult));
        }
    }
    public async Task<GetMedicineLogByIdVm> GetMedicineLogById(GetMedicineLogByIdQuery query)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicine-logs/{query.Id}");
        var stringResults = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetMedicineLogByIdVm>(stringResults);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResults));
        }
    }
    public async Task<GetMedicineLogsByMedicineIdVm> GetMedicineLogsByMedicineId(GetMedicineLogsByMedicineIdQuery query)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .GetAsync($"{_client.BaseAddress}v1/medicine-logs/medicine/{query.MedicineId}");
        var stringResults = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<GetMedicineLogsByMedicineIdVm>(stringResults);
        }
        else
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(stringResults));
        }
    }
    public async Task DeleteMedicineLogsByMedicineId(DeleteMedicineLogsByMedicineIdCommand command)
    {
        await AddAuthorizationHeaderAsync();
        var result = await _client
            .DeleteAsync($"{_client.BaseAddress}v1/medicine-logs/medicine/{command.MedicineId}");
        if (result.StatusCode != System.Net.HttpStatusCode.NoContent)
        {
            throw new ApiUnsuccessfullResultException(DeserializeApiException(await result.Content.ReadAsStringAsync()));
        }
    }

    private async Task AddAuthorizationHeaderAsync()
    {
        if (_accessor.HttpContext is not null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _accessor.HttpContext.GetTokenAsync("access_token") ?? "");
        }
    }
    private static IExceptionResponse DeserializeApiException(string objectToDeserialize)
    {
        return JsonConvert.DeserializeObject<ExceptionResponse>(objectToDeserialize);
    }
}
