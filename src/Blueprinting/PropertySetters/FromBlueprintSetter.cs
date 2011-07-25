using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blueprinting.PropertySetters
{
    public class FromBlueprintSetter : AbstractPropertySetter 
    {
        public FromBlueprintSetter(MemberExpression memberExpression) : base(memberExpression) { }
        
        public override void ApplyValue(object target, IDictionary<string, object> state)
        {
            var provider = Blueprints.BlueprintProvider;
            var getBlueprintMethod = provider.GetType().GetMethod("GetBlueprintFor").MakeGenericMethod(MemberExpression.Type);
            var blueprint = getBlueprintMethod.Invoke(provider, null);
            var objectFromBlueprint = blueprint.GetType().GetMethod("Create").Invoke(blueprint, null);
            SetValue(target, objectFromBlueprint);
        }
    }
}
