using System.Globalization;

namespace JobHunt.Domain.Helper
{
    public class CustomException(string message) : Exception(message) { }

    public class NullObjectException(string message) : Exception(message) { }

    public class AlreadyExistsException(string message) : Exception(message) { }
}
