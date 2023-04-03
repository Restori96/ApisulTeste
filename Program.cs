// See https://aka.ms/new-console-template for more information
using ApiSul;

ElevadorService elevator = new ElevadorService("input.json");
Console.WriteLine("Periodo de maior fluxo do elevador mais frequentado!");

foreach(var elevador in elevator.periodoMaiorFluxoElevadorMaisFrequentado()){
Console.WriteLine(elevador);
}

Console.WriteLine("Andar(es) menos visitado(s)!");
foreach(var elevador in elevator.andarMenosUtilizado()){
Console.WriteLine(elevador);
}

Console.WriteLine("Elevador mais frequentado!");
foreach(var elevador in elevator.elevadorMaisFrequentado()){
Console.WriteLine(elevador);
}

Console.WriteLine("Elevador menos Frequentado!");
foreach(var elevador in elevator.elevadorMenosFrequentado()){
Console.WriteLine(elevador);
}
Console.WriteLine("Periodo de menor fluxo do elevador menos utilizado!");
foreach(var elevador in elevator.periodoMenorFluxoElevadorMenosFrequentado()){
Console.WriteLine(elevador);
}
Console.WriteLine("Periodo de maior utilização do conjunto de elevadores!");
foreach(var elevador in elevator.periodoMaiorUtilizacaoConjuntoElevadores()){
Console.WriteLine(elevador);
}
Console.WriteLine("percentual de utilização do elevador A");
Console.WriteLine(elevator.percentualDeUsoElevadorA().ToString("n2"));

Console.WriteLine("percentual de utilização do elevador B");
Console.WriteLine(elevator.percentualDeUsoElevadorB().ToString("n2"));

Console.WriteLine("percentual de utilização do elevador C");
Console.WriteLine(elevator.percentualDeUsoElevadorC().ToString("n2"));

Console.WriteLine("percentual de utilização do elevador D");
Console.WriteLine(elevator.percentualDeUsoElevadorD().ToString("n2"));

Console.WriteLine("percentual de utilização do elevador E");
Console.WriteLine(elevator.percentualDeUsoElevadorE().ToString("n2"));
