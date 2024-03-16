using AutoMapper;
using Domain;

namespace Application.Services;

public static class CasterProxy
{
    private static readonly IMapper Mapper = Dependency.Get<IMapper>();

    public static T? Cast<T>(this object? obj) where T : class
    {
        return obj is null ? null : (T)Mapper.Map(obj, obj.GetType(), typeof(T));
    }

    public static T? Cast<T>(this object? obj, Action<T> action) where T : class
    {
        if (obj is null) return null;

        var ent = Cast<T>(obj)!;
        action(ent);
        return ent;
    }

    public static IEnumerable<T> CastAll<T>(this IEnumerable<object>? vs) where T : class
    {
        return vs is null
            ? Enumerable.Empty<T>()
            : (IEnumerable<T>)Mapper.Map(vs, vs.GetType(), typeof(IEnumerable<T>));
    }

    public static IEnumerable<T> CastAll<T>(this IEnumerable<object>? vs, Action<T> action) where T : class
    {
        if (vs is null) return Enumerable.Empty<T>();
        var collection = (IEnumerable<T>)Mapper.Map(vs, vs.GetType(), typeof(IEnumerable<T>));
        var castAll = collection as T[] ?? collection.ToArray();
        foreach (var item in castAll) action(item);
        return castAll;
    }
}