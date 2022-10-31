namespace MaterialeShop.Listas
{
    public static class ListaConsts
    {
        private const string DefaultSorting = "{0}Titulo asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Lista." : string.Empty);
        }

        public const int TituloMinLength = 2;
    }
}