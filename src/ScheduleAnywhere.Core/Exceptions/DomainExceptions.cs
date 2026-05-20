namespace ScheduleAnywhere.Core.Exceptions;

public class NotFoundException(string entity, object key)
    : Exception($"{entity} with key '{key}' was not found.");

public class DuplicateNameException(string entity, string name)
    : Exception($"A {entity} named '{name}' already exists.");

public class DuplicateAbbreviationException(string entity, string abbreviation)
    : Exception($"A {entity} with abbreviation '{abbreviation}' already exists.");

public class ValidationException(string message) : Exception(message);

public class UnauthorizedException(string message = "Access denied.") : Exception(message);

public class OverlappingShiftException()
    : Exception("This shift overlaps an existing shift for the same employee on this date.");

public class InvalidCredentialsException()
    : Exception("Username or password is incorrect.");
