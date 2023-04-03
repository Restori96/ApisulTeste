
using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace ApiSul
{
    public class ElevadorService : IElevadorService
    {
        private List<RegistroElevador> registros;

        public ElevadorService(string caminhoArquivo)
        {
            // Leitura do arquivo json
            using (StreamReader r = new StreamReader(caminhoArquivo))
            {
                string json = r.ReadToEnd();
                this.registros = JsonConvert.DeserializeObject<List<RegistroElevador>>(json);
            }
        }

        public List<int> andarMenosUtilizado()
        {
            Dictionary<int, int> contagemAndares = new Dictionary<int, int>();
            foreach (var registro in registros)
            {
                if (contagemAndares.ContainsKey(registro.Andar))
                {
                    contagemAndares[registro.Andar]++;
                }
                else
                {
                    contagemAndares[registro.Andar] = 1;
                }
            }

            // Encontrar os andares menos utilizados
            int menorFrequencia = contagemAndares.Values.Min();
            List<int> andaresMenosUtilizados = contagemAndares.Where(dic => dic.Value == menorFrequencia)
                                                              .Select(dic => dic.Key)
                                                              .ToList();

            // Retornar a lista contendo os andares menos utilizados
            return andaresMenosUtilizados;
        }

        public List<char> elevadorMaisFrequentado()
        {
            Dictionary<char, int> contagemElevadores = new Dictionary<char, int>();


            foreach (var registro in registros)
            {
                char elevator = registro.Elevador;
                if (contagemElevadores.ContainsKey(elevator))
                {
                    contagemElevadores[elevator]++;
                }
                else
                {
                    contagemElevadores[elevator] = 1;
                }
            }

            List<char> elevadoresMaisFrequentes = new List<char>();
            int maxCount = 0;

            // Encontra Elevadores mais Utilizados
            foreach (var elevador in contagemElevadores)
            {
                if (elevador.Value > maxCount)
                {
                    elevadoresMaisFrequentes.Clear();
                    elevadoresMaisFrequentes.Add(elevador.Key);
                    maxCount = elevador.Value;
                }
                else if (elevador.Value == maxCount)
                {
                    elevadoresMaisFrequentes.Add(elevador.Key);
                }
            }

            return elevadoresMaisFrequentes;
        }
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var elevadorMaisFrequentado = this.elevadorMaisFrequentado();
            var periodoMaiorFluxo = new List<char>();
            var contagemPorElevadorEPeriodo = new Dictionary<char, Dictionary<char, int>>();

            // Inicializa o dicionário de contagem para cada elevador e período
            foreach (var elevador in elevadorMaisFrequentado)
            {
                contagemPorElevadorEPeriodo.Add(elevador, new Dictionary<char, int>());
                contagemPorElevadorEPeriodo[elevador].Add('M', 0);
                contagemPorElevadorEPeriodo[elevador].Add('V', 0);
                contagemPorElevadorEPeriodo[elevador].Add('N', 0);
            }

            // Conta a ocorrência de cada período para cada elevador
            foreach (var chamada in registros)
            {
                if (elevadorMaisFrequentado.Contains(chamada.Elevador))
                    contagemPorElevadorEPeriodo[chamada.Elevador][chamada.Turno]++;
            }

            // Encontra o período com a maior contagem para cada elevador mais frequentado
            foreach (var elevador in elevadorMaisFrequentado)
            {
                var maxContagem = contagemPorElevadorEPeriodo[elevador].Values.Max();
                periodoMaiorFluxo.AddRange(contagemPorElevadorEPeriodo[elevador].Where(kv => kv.Value == maxContagem).Select(kv => kv.Key));
            }

            return periodoMaiorFluxo.Distinct().ToList();
        }



        public List<char> elevadorMenosFrequentado()
        {
            Dictionary<char, int> contagemElevadores = new Dictionary<char, int>();


            foreach (var registro in registros)
            {
                char elevator = registro.Elevador;
                if (contagemElevadores.ContainsKey(elevator))
                {
                    contagemElevadores[elevator]++;
                }
                else
                {
                    contagemElevadores[elevator] = 1;
                }
            }

            List<char> elevadoresMenosFrequentes = new List<char>();
            int minCount = int.MaxValue;

            // Encontra Elevadores menos Utilizados
            foreach (var elevador in contagemElevadores)
            {
                if (elevador.Value < minCount)
                {
                    elevadoresMenosFrequentes.Clear();
                    elevadoresMenosFrequentes.Add(elevador.Key);
                    minCount = elevador.Value;
                }
                else if (elevador.Value == minCount)
                {
                    elevadoresMenosFrequentes.Add(elevador.Key);
                }
            }

            return elevadoresMenosFrequentes;
        }
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            Dictionary<char, Dictionary<char, int>> elevadorPeriodoCount = new Dictionary<char, Dictionary<char, int>>();

   
            foreach (var registro in registros)
            {
                if (!elevadorPeriodoCount.ContainsKey(registro.Elevador))
                {
                    elevadorPeriodoCount[registro.Elevador] = new Dictionary<char, int>();
                }
                if (!elevadorPeriodoCount[registro.Elevador].ContainsKey(registro.Turno))
                {
                    elevadorPeriodoCount[registro.Elevador][registro.Turno] = 0;
                }
                elevadorPeriodoCount[registro.Elevador][registro.Turno]++;
            }

            // Encontra elevadores menos frequentados
            List<char> menosFrequentados = new List<char>();
            int menorContagem = int.MaxValue;
            foreach (var elevadorPeriodo in elevadorPeriodoCount)
            {
                int contagem = elevadorPeriodo.Value.Values.Sum();
                if (contagem < menorContagem)
                {
                    menosFrequentados.Clear();
                    menosFrequentados.Add(elevadorPeriodo.Key);
                    menorContagem = contagem;
                }
                else if (contagem == menorContagem)
                {
                    menosFrequentados.Add(elevadorPeriodo.Key);
                }
            }

            // For each least frequent elevator, find the least frequent period
            List<char> periodosMenorFluxo = new List<char>();
            menorContagem = int.MaxValue;
            foreach (char elevador in menosFrequentados)
            {
                foreach (var elevadorPeriodo in elevadorPeriodoCount[elevador])
                {
                    int contagem = elevadorPeriodo.Value;
                    if (contagem < menorContagem)
                    {
                        periodosMenorFluxo.Clear();
                        periodosMenorFluxo.Add(elevadorPeriodo.Key);
                        menorContagem = contagem;
                    }
                    else if (contagem == menorContagem)
                    {
                        periodosMenorFluxo.Add(elevadorPeriodo.Key);
                    }
                }
            }

            return periodosMenorFluxo;
        }
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            Dictionary<char, int> periodosCount = new Dictionary<char, int>();
            int maxCount = 0;

            foreach (var registro in registros)
            {
                char periodo = registro.Turno;

                if (periodosCount.ContainsKey(periodo))
                {
                    periodosCount[periodo]++;
                }
                else
                {
                    periodosCount[periodo] = 1;
                }

                if (periodosCount[periodo] > maxCount)
                {
                    maxCount = periodosCount[periodo];
                }
            }

            List<char> periodosMaisUtilizados = new List<char>();
            foreach (KeyValuePair<char, int> periodoCount in periodosCount)
            {
                if (periodoCount.Value == maxCount)
                {
                    periodosMaisUtilizados.Add(periodoCount.Key);
                }
            }

            return periodosMaisUtilizados;

        }
        public float percentualDeUsoElevadorA()
        {
            float totalServicos = registros.Count;
            float servicosElevadorA = registros.Where(d => d.Elevador == 'A').Count();

            return (servicosElevadorA / totalServicos) * 100;
        }
        public float percentualDeUsoElevadorB()
        {
            float totalServicos = registros.Count;
            float servicosElevadorA = registros.Where(d => d.Elevador == 'B').Count();

            return (servicosElevadorA / totalServicos) * 100;
        }
        public float percentualDeUsoElevadorC()
        {
            float totalServicos = registros.Count;
            float servicosElevadorA = registros.Where(d => d.Elevador == 'C').Count();

            return (servicosElevadorA / totalServicos) * 100;
        }
        public float percentualDeUsoElevadorD()
        {
            float totalServicos = registros.Count;
            float servicosElevadorA = registros.Where(d => d.Elevador == 'D').Count();

            return (servicosElevadorA / totalServicos) * 100;
        }
        public float percentualDeUsoElevadorE()
        {
            float totalServicos = registros.Count;
            float servicosElevadorA = registros.Where(d => d.Elevador == 'E').Count();

            return (servicosElevadorA / totalServicos) * 100;
        }
    }
}
