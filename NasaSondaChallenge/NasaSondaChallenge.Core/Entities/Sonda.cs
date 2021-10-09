using System.Collections.Generic;

namespace NasaSondaChallenge.Core.Entities
{
    public class Sonda
    {
        public Sonda()
        {
            Comandos = new List<char>();
        }

        public Posicao Posicao { get; set; }
        public List<char> Comandos { get; set; }
    }
}
