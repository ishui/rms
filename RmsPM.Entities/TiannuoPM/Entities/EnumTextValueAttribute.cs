namespace TiannuoPM.Entities
{
    using System;

    public sealed class EnumTextValueAttribute : Attribute
    {
        private readonly string enumTextValue;

        public EnumTextValueAttribute(string text)
        {
            this.enumTextValue = text;
        }

        public string Text
        {
            get
            {
                return this.enumTextValue;
            }
        }
    }
}

