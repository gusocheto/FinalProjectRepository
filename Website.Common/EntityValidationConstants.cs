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

        public static class OderDetails
        {
            public const byte CityNameMinLength = 1;
            public const byte CityNameMaxLength = 200;
            public const byte CountryNameMinLength = 4;
            public const byte CountryNameMaxLength = 60;
        }
    }
}
