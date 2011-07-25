using System.Collections.Generic;

namespace Blueprinting
{
    public interface IBlueprint<out T> where T : class
    {
        T Create();
        IDictionary<string, object> State { get; }
    }
}
