using xadrez_console.Entities.Enums;

namespace xadrez_console.Entities
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarMovimentos()
        {
            QuantidadeMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
