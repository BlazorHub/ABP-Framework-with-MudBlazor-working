namespace MaterialeShop.Enderecos
{
    public static class EnderecoConsts
    {
        private const string DefaultSorting = "{0}EnderecoCompleto asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Endereco." : string.Empty);
        }

        public const int EnderecoCompletoMinLength = 2;
    }
}