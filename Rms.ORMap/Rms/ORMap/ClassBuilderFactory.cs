namespace Rms.ORMap
{
    using System;

    public sealed class ClassBuilderFactory
    {
        private ClassBuilderFactory()
        {
        }

        public static IClassBuilder GetClassBuilder(string builderName)
        {
            switch (builderName)
            {
                case "Single":
                    return new SingleTableClassBuilder();

                case "Standard":
                    return new StandardTableClassBuilder();
            }
            return new SingleTableClassBuilder();
        }
    }
}

