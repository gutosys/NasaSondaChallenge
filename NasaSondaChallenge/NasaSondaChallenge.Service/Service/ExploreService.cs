using NasaSondaChallenge.Core.Entities;
using NasaSondaChallenge.Service.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NasaSondaChallenge.Service.Service
{
    public class ExploreService : IExploreService
    {
        public void Explore()
        {
            try
            {
                using (var sr = new StreamReader("entrada.txt"))
                {
                    var entrada = sr.ReadToEnd();

                    List<string> lines = entrada.Split(
                        new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.None
                    ).ToList();

                    var coordenadaqSuperiorDireito = lines.FirstOrDefault();
                    var sondaList = SondaList(lines);

                    var sondaResult = MovimentaSonda(sondaList);

                    EscreveSaida(sondaResult);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void EscreveSaida(List<Sonda> sondaResult)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\saida.txt";
            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (var line in sondaResult)
                {
                    sw.WriteLine($"{line.Posicao.PosicaoX} { line.Posicao.PosicaoY} {line.Posicao.DirecaoCardinal}");
                }
            }
        }

        private List<Sonda> MovimentaSonda(List<Sonda> sondaList)
        {
            const char move = 'M';
            const char left = 'L';
            const char right = 'R';
            var north = "N";
            var south = "S";
            var east = "E";
            var west = "W";

            foreach (var sonda in sondaList)
            {
                foreach (var comando in sonda.Comandos)
                {
                    var direcaoCardinal = sonda.Posicao.DirecaoCardinal;

                    switch (comando)
                    {
                        case move:
                            Move(north, south, east, west, sonda, direcaoCardinal);
                            break;
                        case left:
                            TurnLeft(north, south, east, west, sonda, direcaoCardinal);
                            break;
                        case right:
                            TurnRight(north, south, east, sonda, direcaoCardinal);
                            break;
                    }
                }
            }

            return sondaList;
        }

        private static void Move(string north, string south, string east, string west, Sonda sonda, string direcaoCardinal)
        {
            if (direcaoCardinal == north)
            {
                sonda.Posicao.PosicaoY = sonda.Posicao.PosicaoY + 1;
            }
            else if (direcaoCardinal == south)
            {
                sonda.Posicao.PosicaoY = sonda.Posicao.PosicaoY - 1;
            }
            else if (direcaoCardinal == east)
            {
                sonda.Posicao.PosicaoX = sonda.Posicao.PosicaoX + 1;
            }
            else if (direcaoCardinal == west)
            {
                sonda.Posicao.PosicaoX = sonda.Posicao.PosicaoX - 1;
            }
        }

        private static void TurnLeft(string north, string south, string east, string west, Sonda sonda, string direcaoCardinal)
        {
            if (direcaoCardinal == north)
            {
                sonda.Posicao.DirecaoCardinal = west;
            }
            else if (direcaoCardinal == south)
            {
                sonda.Posicao.DirecaoCardinal = east;
            }
            else if (direcaoCardinal == east)
            {
                sonda.Posicao.DirecaoCardinal = north;
            }
            else if (direcaoCardinal == west)
            {
                sonda.Posicao.DirecaoCardinal = south;
            }
        }

        private static void TurnRight(string north, string south, string east, Sonda sonda, string direcaoCardinal)
        {
            if (direcaoCardinal == north)
            {
                sonda.Posicao.DirecaoCardinal = east;
            }
            else if (direcaoCardinal == south)
            {
                sonda.Posicao.DirecaoCardinal = "W";
            }
            else if (direcaoCardinal == east)
            {
                sonda.Posicao.DirecaoCardinal = south;
            }
            else if (direcaoCardinal == "W")
            {
                sonda.Posicao.DirecaoCardinal = north;
            }
        }

        private static List<Sonda> SondaList(List<string> lines)
        {
            List<Sonda> sondaList = new List<Sonda>();
            lines.RemoveAt(0);
            var resultSondaList = lines.Split(2).ToList();
            foreach (var sondaItem in resultSondaList)
            {
                var posicaoInicial = sondaItem.FirstOrDefault();
                var comandos = sondaItem.LastOrDefault().ToCharArray().ToList();
                var elementos = posicaoInicial.Split(" ");

                sondaList.Add(new Sonda
                {
                    Posicao = new Posicao()
                    {
                        PosicaoX = Convert.ToInt32(elementos[0]),
                        PosicaoY = Convert.ToInt32(elementos[1]),
                        DirecaoCardinal = elementos[2]
                    },
                    Comandos = new List<char>(comandos)
                });
            }

            return sondaList;
        }
    }

    public static class ListExtensionMethod
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> sourceList, int ListSize)
        {
            while (sourceList.Any())
            {
                yield return sourceList.Take(ListSize);
                sourceList = sourceList.Skip(ListSize);
            }
        }
    }

}
