using System;
using xadrez_console.Entities;
using xadrez_console.Entities.Enums;
using xadrez_console.Exceptions;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);
            // Console.WriteLine(posicaoXadrez);
            // Console.WriteLine(posicaoXadrez.ToPosicao());

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            try
            {
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Branca), new Posicao(2, 4));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 5));

                Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
