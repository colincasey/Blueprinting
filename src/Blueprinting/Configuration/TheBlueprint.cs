using System;

namespace Blueprinting.Configuration
{
    public class TheBlueprint
    {
        public static FromTypeDescriptor From<T>()
        {
            return new FromTypeDescriptor(typeof(T));
        }

        public static FromTypeDescriptor From(Type t)
        {
            return new FromTypeDescriptor(t);
        }
    }
}
