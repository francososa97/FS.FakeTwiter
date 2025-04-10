namespace FS.FakeTwitter.Application.Exceptions;

public class NotFoundException : AppException
{

    public NotFoundException(string entity)
        : base($"{entity} was not found.", 404)
    {
    }

    public NotFoundException(string entity, object key)
        : base($"{entity} with key '{key}' was not found.", 404)
    {
    }
}
