using System.IO;

namespace LeitorKindle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<-----Leitor de anotações do Kindle para livros não oficiais>");

            string arquivoFonte = (@"C:\Kindle\My Clippings.txt");
            string arquivoDestino = (@"C:\Kindle\AnotacoesFinalizadas.txt");

            MudarCorConsole($"Digite o nome do livro que deseja filtrar, exatamente como está escrito no arquivo do Kindle:", ConsoleColor.Yellow);

            string livroNome = Console.ReadLine();

            try
            {
                List<string> anotacoesFiltradas = new List<string>();

                using (var leitor = new StreamReader(arquivoFonte))
                {
                    string linha;
                    string anotacaoAtual = string.Empty;

                    while ((linha = leitor.ReadLine()) != null)
                    {
                        if (linha == "==========")
                        {
                            if (anotacaoAtual.Contains(livroNome, StringComparison.OrdinalIgnoreCase))
                            {
                                anotacoesFiltradas.Add(anotacaoAtual);
                            }

                            anotacaoAtual = string.Empty;
                        }
                        else
                        {
                            anotacaoAtual += linha + Environment.NewLine;
                        }
                    }
                }

                if (anotacoesFiltradas.Count > 0)
                {
                    File.WriteAllLines(arquivoDestino, anotacoesFiltradas);
                    MudarCorConsole($"Anotações do livro \"{livroNome}\" foram salvas em {arquivoDestino}.", ConsoleColor.Green);
                }
                else
                {
                    MudarCorConsole($"Nenhuma anotação encontrada para o livro \"{livroNome}\".", ConsoleColor.Yellow);
                }
            }
            catch (Exception ex)
            {
                MudarCorConsole($"Erro ao processar o arquivo: {ex.Message}", ConsoleColor.Red);
            }
        }
        public static void MudarCorConsole(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}
