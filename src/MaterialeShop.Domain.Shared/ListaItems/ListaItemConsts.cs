namespace MaterialeShop.ListaItems
{
    public static class ListaItemConsts
    {
        private const string DefaultSorting = "{0}Descricao asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ListaItem." : string.Empty);
        }

        public const int DescricaoMinLength = 2;
    }
}