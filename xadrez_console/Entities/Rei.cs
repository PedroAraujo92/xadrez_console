using xadrez_console.Entities.Enums;

namespace xadrez_console.Entities
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
            
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
