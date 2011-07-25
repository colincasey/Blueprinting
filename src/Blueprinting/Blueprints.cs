using System;
using System.Linq;
using Blueprinting.Configuration;

namespace Blueprinting
{
    public static class Blueprints 
    {
        private static IBlueprintProvider _blueprintProvider;

        public static IBlueprintProvider BlueprintProvider
        {
            get
            {
                if (_blueprintProvider == null)
                {
                    var blueprintRegistrations = AutomaticallyRegisterAllBlueprints();
                    _blueprintProvider = new DefaultBlueprintProvider(blueprintRegistrations);
                }
                return _blueprintProvider;
            }

            set { _blueprintProvider = value; }
        }

        public static void Clear()
        {
            BlueprintProvider = null;
        }

        public static Builder<T> For<T>() where T : class
        {
            return BlueprintProvider.CreateBuilder<T>();
        }

        private static IBlueprintRegistration[] AutomaticallyRegisterAllBlueprints()
        {
            return AppDomain.CurrentDomain.GetAssemblies().
                Select(AllBlueprints.FromAssembly).
                Cast<IBlueprintRegistration>().
                ToArray();
        }
    }
}
