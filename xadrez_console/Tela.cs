using System;
using System.Collections.Generic;
using System.Text;
using xadrez_console.Entities;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.Pecas[i, j] == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.Pecas[i, j] + " ");
                    }                    
                }
                Console.WriteLine();
            }
        }
    }
}
