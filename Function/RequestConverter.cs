using Application.Abstract;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace Function;

public static class RequestConverter
{
    public static T Convert<T>(this HttpRequestData req) where T : class
    {
        
        var dtoString = req.ReadAsStringAsync().Result;
        // convert via Newtonsoft.Json
        var dto = JsonConvert.DeserializeObject<T>(dtoString!);
        // Validate with FluentValidation
        if (dto is IValidatable<T>) (dto as IValidatable<T>)!.Validator.ValidateAndThrow(dto);
        return dto!;
    } 
}