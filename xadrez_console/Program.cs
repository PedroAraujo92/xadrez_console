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
            try
            {
                PartidaXadrez partidaXadrez = new PartidaXadrez();

                while (!partidaXadrez.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaXadrez.Tabuleiro);

                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partidaXadrez.Turno);
                        Console.WriteLine("Aguardando jogada: " + partidaXadrez.JogadorAtual);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaXadrez.ValidarPosicaoOrigem(origem);


                        bool[,] posicoesPossiveis = partidaXadrez.Tabuleiro.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaXadrez.Tabuleiro, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partidaXadrez.ValidarPosicaoDestino(origem, destino);

                        partidaXadrez.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Tela.ImprimirTabuleiro(partidaXadrez.Tabuleiro);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
