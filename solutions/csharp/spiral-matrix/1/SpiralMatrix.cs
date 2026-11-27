using System;

public class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size));

        var matrix = new int[size, size];

        // Напрямки за годинниковою стрілкою: вправо, вниз, вліво, вгору
        int[] dRow = { 0, 1, 0, -1 };
        int[] dCol = { 1, 0, -1, 0 };

        int row = 0, col = 0, dir = 0;

        for (int value = 1; value <= size * size; value++)
        {
            matrix[row, col] = value;

            int nextRow = row + dRow[dir];
            int nextCol = col + dCol[dir];

            // Якщо наступна клітинка поза межами або вже заповнена, повертаємо
            bool blocked = nextRow < 0 || nextRow >= size
                        || nextCol < 0 || nextCol >= size
                        || matrix[nextRow, nextCol] != 0;

            if (blocked)
            {
                dir = (dir + 1) % 4;
                nextRow = row + dRow[dir];
                nextCol = col + dCol[dir];
            }

            row = nextRow;
            col = nextCol;
        }

        return matrix;
    }
}