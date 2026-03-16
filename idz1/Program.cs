using System;

while (true)
{
    Console.Write("Введите первую строку (exit для выхода): ");
    string? str1 = Console.ReadLine();

    if (str1 == null)
    {
        Console.WriteLine("Ошибка ввода");
        continue;
    }

    if (str1.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Завершение программы");
        break;
    }

    Console.Write("Введите вторую строку: ");
    string? str2 = Console.ReadLine();

    if (str2 == null)
    {
        Console.WriteLine("Ошибка ввода");
        continue;
    }

    int distance = DamerauLevenshteinDistance(str1, str2);
    Console.WriteLine($"Расстояние Дамерау-Левенштейна между \"{str1}\" и \"{str2}\" = {distance}");
    Console.WriteLine();
}

static int DamerauLevenshteinDistance(string str1Param, string str2Param)
{
    if (str1Param == null || str2Param == null)
        return -1;

    int str1Len = str1Param.Length;
    int str2Len = str2Param.Length;

    if (str1Len == 0 && str2Len == 0) return 0;
    if (str1Len == 0) return str2Len;
    if (str2Len == 0) return str1Len;

    string str1 = str1Param.ToUpper();
    string str2 = str2Param.ToUpper();

    int[,] matrix = new int[str1Len + 1, str2Len + 1];

    for (int i = 0; i <= str1Len; i++)
        matrix[i, 0] = i;

    for (int j = 0; j <= str2Len; j++)
        matrix[0, j] = j;

    for (int i = 1; i <= str1Len; i++)
    {
        for (int j = 1; j <= str2Len; j++)
        {
            int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

            int insert = matrix[i, j - 1] + 1;
            int delete = matrix[i - 1, j] + 1;
            int replace = matrix[i - 1, j - 1] + cost;

            matrix[i, j] = Math.Min(Math.Min(insert, delete), replace);

            if (i > 1 && j > 1 &&
                str1[i - 1] == str2[j - 2] &&
                str1[i - 2] == str2[j - 1])
            {
                matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + 1);
            }
        }
    }

    return matrix[str1Len, str2Len];
}
