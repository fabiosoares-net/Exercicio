using Newtonsoft.Json;
using Questao2;
using System;
using System.Runtime.CompilerServices;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint-Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        JogoFutebolDTO jogoFutebolDTO = null;
        int numeroDeGols = 0;
        bool existeTime = true;
        int page = 0;

        try
        {
            while (existeTime)
            {
                var httpClient = new HttpClient();

                HttpResponseMessage response = httpClient
                    .GetAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    jogoFutebolDTO = JsonConvert.DeserializeObject<JogoFutebolDTO>(responseBody);

                    if (jogoFutebolDTO.Time != null && jogoFutebolDTO.Time.Any())
                    {
                        var totalGols = jogoFutebolDTO.Time.Select(x => x.Team1goals).Sum();

                        numeroDeGols += totalGols;
                        page++;
                    }
                    else
                    {
                        existeTime = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return numeroDeGols;
    }
}