using System;
using System.Collections.Generic;
using Blueprinting.Configuration;
using Blueprinting.PropertySetters;
using Blueprinting.Util;

namespace Blueprinting
{
    public class DefaultBlueprintProvider : IBlueprintProvider
    {
        private readonly Dictionary<Type, object> _registeredBlueprints = new Dictionary<Type, object>();

        public DefaultBlueprintProvider(params IBlueprintRegistration[] blueprintRegistrations)
        {
            foreach (var blueprintRegistration in blueprintRegistrations)
            {
                blueprintRegistration.Register(this);
            }
        }

        public void Add<T>()
        {
            Add(typeof(T));
        }

        public void Add(Type blueprintType)
        {
            if(!blueprintType.IsBlueprintType()) throw new ArgumentException("Provided type does not implement IBlueprint<T>", "blueprintType");

            var blueprintFor = blueprintType.GetBlueprintedType();
            var blueprint = Activator.CreateInstance(blueprintType, true);
            _registeredBlueprints[blueprintFor] = blueprint;
        }

        public IBlueprint<T> GetBlueprintFor<T>() where T : class
        {
            var blueprintedType = typeof (T);
            if (_registeredBlueprints.ContainsKey(blueprintedType))
            {
                return (IBlueprint<T>) _registeredBlueprints[blueprintedType];
            }
            throw new Exception("no blueprint registered for type " + blueprintedType.FullName);
        }

        public Builder<T> CreateBuilder<T>() where T : class
        {
            return new Builder<T>(GetBlueprintFor<T>());
        }
    }
}
