namespace todo.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string operation): base($"Operation '{operation}' failed.") { }
    }
}
