using Xunit.Abstractions;

namespace Stofipy.BL.Tests.Helpers;

public class NullTestOutputHelper : ITestOutputHelper
{
    public void WriteLine(string message) { }
    public void WriteLine(string format, params object[] args) { }
}