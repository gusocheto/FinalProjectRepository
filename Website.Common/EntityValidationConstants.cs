namespace Website.Common
{
    public static class EntityValidationConstants
    {
        public static class CustomerUser
        {
            public const byte CustomerNameMinLength = 2;
            public const byte CustomerNameMaxLength = 40;
        }

        public static class Product
        {
            public const byte ProductNameMinLength = 3;
            public const byte ProductNameMaxLength = 70;
            public const byte ProductDetailsMinLength = 10;
            public const byte ProductDetailsMaxLength = byte.MaxValue;
        }
    }
}
