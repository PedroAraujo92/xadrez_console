using System.Collections.Generic;
using System.Text;
using xadrez_console.Entities.Enums;
using xadrez_console.Entities.Pecas;
using xadrez_console.Exceptions;

namespace xadrez_console.Entities
{
    class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> Pecas { get; private set; } = new HashSet<Peca>();
        public HashSet<Peca> Capturadas { get; private set; } = new HashSet<Peca>();
        public bool Xeque { get; private set; } = false;

        public PartidaXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
            Terminada = false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarMovimentos();
            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(peca, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstarEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }

            if (EstarEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoOrigem(Posicao origem)
        {
            if (Tabuleiro.Peca(origem) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tabuleiro.Peca(origem).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tabuleiro.Peca(origem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturas(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();
            foreach (Peca item in Capturadas)
            {
                if(item.Cor == cor)
                {
                    pecasAux.Add(item);
                }
            }

            return pecasAux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();
            foreach (Peca item in Pecas)
            {
                if (item.Cor == cor)
                {
                    pecasAux.Add(item);
                }
            }

            pecasAux.ExceptWith(PecasCapturas(cor));
            return pecasAux;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }

            return Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca item in PecasEmJogo(cor))
            {
                if (item is Rei)
                {
                    return item;
                }
            }

            return null;
        }

        public bool EstarEmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if(r == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca item in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = item.MovimentosPossiveis();
                if(matriz[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tabuleiro, Cor.Preta));
        }
    }
}
