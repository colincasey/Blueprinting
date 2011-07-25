using System;

namespace Blueprinting
{
    public interface IBlueprintProvider
    {
        void Add<TBlueprint>();
        void Add(Type blueprintType);
        Builder<T> CreateBuilder<T>() where T : class;
        IBlueprint<T> GetBlueprintFor<T>() where T : class;
    }
}
