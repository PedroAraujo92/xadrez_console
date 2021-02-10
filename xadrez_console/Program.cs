using System;
using xadrez_console.Entities;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3, 4);
            Console.WriteLine("Posição: " + p);

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(tabuleiro);
        }
    }
}
