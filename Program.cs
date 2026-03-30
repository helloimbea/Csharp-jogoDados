using System;
using System.Threading;
using static System.Console;

Jogador jogador1 = new Jogador();
Jogador jogador2 = new Jogador();

while (true)
{
    Clear();
    WriteLine("===== JOGO DOS DADOS =====\n");

    jogador1.Nome = PedirNome("Jogador 1");
    jogador2.Nome = PedirNome("Jogador 2");

    if (jogador1.Nome == jogador2.Nome)
    {
        WriteLine("\nOs nomes precisam ser diferentes!");
        Thread.Sleep(1500);
        continue;
    }

    jogador1.Reset();
    jogador2.Reset();

    for (int i = 0; i < 3; i++)
    {
        WriteLine($"\nRodada {i + 1}");

        jogador1.Numeros[i] = Jogar(jogador1);
        jogador2.Numeros[i] = Jogar(jogador2);

        VerificarRodada(jogador1, jogador2, i);
    }

    MostrarResultadoFinal(jogador1, jogador2);

    Write("\nJogar novamente? (s/n): ");
    string resposta = ReadLine().ToLower();

    if (resposta != "s")
        break;
}

string PedirNome(string jogador)
{
    Write($"{jogador}, informe seu nome: ");
    return ReadLine();
}

int Jogar(Jogador jogador)
{
    WriteLine($"\n{jogador.Nome}, pressione ENTER para jogar o dado");
    ReadLine();

    AnimacaoCarregamento();

    int numero = jogador.RolarDado();
    WriteLine($"Resultado: {numero}");

    return numero;
}

void VerificarRodada(Jogador j1, Jogador j2, int rodada)
{
    if (j1.Numeros[rodada] > j2.Numeros[rodada])
    {
        j1.Pontos++;
        WriteLine($"{j1.Nome} venceu a rodada!");
    }
    else if (j2.Numeros[rodada] > j1.Numeros[rodada])
    {
        j2.Pontos++;
        WriteLine($"{j2.Nome} venceu a rodada!");
    }
    else
    {
        WriteLine("Empate!");
    }
}

void MostrarResultadoFinal(Jogador j1, Jogador j2)
{
    WriteLine("\n===== RESULTADO FINAL =====");
    WriteLine($"{j1.Nome}: {j1.Pontos} pontos");
    WriteLine($"{j2.Nome}: {j2.Pontos} pontos");

    if (j1.Pontos > j2.Pontos)
        WriteLine($"\n{j1.Nome} é o grande vencedor!");
    else if (j2.Pontos > j1.Pontos)
        WriteLine($"\n{j2.Nome} é o grande vencedor!");
    else
        WriteLine("\nO jogo terminou em empate!");
}

void AnimacaoCarregamento()
{
    string[] anim = { "|", "/", "-", "\\" };

    for (int i = 0; i < 8; i++)
    {
        Write($"\rGirando {anim[i % anim.Length]}");
        Thread.Sleep(150);
    }

    WriteLine();
}

public class Jogador
{
    private static Random rnd = new Random();

    public string Nome { get; set; }
    public int[] Numeros { get; set; } = new int[3];
    public int Pontos { get; set; }

    public int RolarDado()
    {
        return rnd.Next(1, 7);
    }

    public void Reset()
    {
        Numeros = new int[3];
        Pontos = 0;
    }
}