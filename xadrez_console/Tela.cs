using System;
using System.Collections.Generic;
using System.Text;
using xadrez_console.Entities;
using xadrez_console.Entities.Enums;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partidaXadrez)
        {
            ImprimirTabuleiro(partidaXadrez.Tabuleiro);

            Console.WriteLine();
            ImprimirPecasCapturadas(partidaXadrez);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partidaXadrez.Turno);
            Console.WriteLine("Aguardando jogada: " + partidaXadrez.JogadorAtual);
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partidaXadrez)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brandas: ");
            ImprimirConjunto(partidaXadrez.PecasCapturas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor original = Console.ForegroundColor;                        
            Console.ForegroundColor = ConsoleColor.Red;
            ImprimirConjunto(partidaXadrez.PecasCapturas(Cor.Preta));
            Console.ForegroundColor = original;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca item in conjunto)
            {
                Console.Write(item + " ");
            }
            Console.Write("]");
        }


        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Pecas[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor original = Console.BackgroundColor;
            ConsoleColor possivel = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = possivel;
                    }
                    else
                    {
                        Console.BackgroundColor = original;
                    }
                    ImprimirPeca(tabuleiro.Pecas[i, j]);
                    Console.BackgroundColor = original;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = original;
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}
