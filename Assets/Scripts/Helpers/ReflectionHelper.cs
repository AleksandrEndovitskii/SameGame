namespace Helpers
{
    public static class ReflectionHelper
    {
        public static string GetCallerMemberName([System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "")
        {
            return callerMemberName;
        }
    }
}
