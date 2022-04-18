namespace ProjetoCompeticao.Domain.Academias.Extensions
{
    public static class AcademiaMessagesExtensions
    {
        public static string InsercaoDeAcademia() => "Nova Academia cadastrada com sucesso.";
        public static string AtualizacaoDeAcademia() => "Dados da Academia atualizados com sucesso.";
        public static string ExclusaoDeAcademia() => "Dados da Academia exluídos com sucesso.";
        public static string ErroInsercaoAcademia() => "Problemas ao salvar a academia.";
        public static string ErroAtualizacaoAcademia() => "Problemas ao atualizar os dados da academia.";
        public static string AcademiaNaoExiste() => "A Academia não existe";
        public static string ErroExclusaoAcademia() => "Problemas ao excluir a academia.";
        public static string AcademiaJaEstaCadastrada() => "Já existe uma academia cadastrada com esse nome.";
        public static string PesquisaDeAcademiasSemRetorno() => "A pesquisa não retornou resultados";
        public static string ErroPesquisaAcademia() => "Problemas ao Listar a academia";
    }
}
