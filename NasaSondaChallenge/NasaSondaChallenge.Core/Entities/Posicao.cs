using NasaSondaChallenge.Core.Enums;

namespace NasaSondaChallenge.Core.Entities
{
    public class Posicao
    {
        public string PosicaoX { get; set; }
        public string PosicaoY { get; set; }
        public DirecaoCardinalEnum DirecaoCardinal { get; set; }
    }
}
