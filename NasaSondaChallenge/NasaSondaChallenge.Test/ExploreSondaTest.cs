using Microsoft.VisualStudio.TestTools.UnitTesting;
using NasaSondaChallenge.Core.Entities;
using NasaSondaChallenge.Service.Service;
using System.Collections.Generic;
using System.Linq;

namespace NasaSondaChallenge.Test
{
    [TestClass]
    public class ExploreSondaTest
    {
        [TestMethod]
        public void MovimentaSondaTest()
        {
            ExploreService sonda = new ExploreService();

            //// Arrange
            List<Sonda> sondaList = new List<Sonda>();
            sondaList.Add(new Sonda
            {
                Comandos = new List<char> { 'L', 'M', 'L', 'M', 'L', 'M', 'L', 'M', 'M' },
                Posicao = new Posicao { PosicaoX = 1, PosicaoY = 2, DirecaoCardinal = "N" }
            });

            //// Act
            var result = sonda.MovimentaSonda(sondaList).FirstOrDefault();
            
            //// Assert            
            Assert.AreEqual(result.Posicao.PosicaoX, 1, "A posi��o x n�o est� ok");
            Assert.AreEqual(result.Posicao.PosicaoY, 3, "A posi��o y n�o est� ok");
            Assert.AreEqual(result.Posicao.DirecaoCardinal, "N", "A dire��o cardinal n�o est� ok");            
        }
    }
}
