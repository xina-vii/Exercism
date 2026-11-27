using System;

public class CircularBuffer<T>
{
    private readonly T[] _buffer;
    private int _head;   // індекс найстарішого елемента
    private int _count;  // кількість елементів у буфері

    public CircularBuffer(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _buffer = new T[capacity];
    }

    public T Read()
    {
        if (_count == 0)
            throw new InvalidOperationException("Буфер порожній");

        T value = _buffer[_head];
        _buffer[_head] = default!;               // не тримаємо зайве посилання
        _head = (_head + 1) % _buffer.Length;
        _count--;
        return value;
    }

    public void Write(T value)
    {
        if (_count == _buffer.Length)
            throw new InvalidOperationException("Буфер заповнений");

        _buffer[(_head + _count) % _buffer.Length] = value;
        _count++;
    }

    public void Overwrite(T value)
    {
        if (_buffer.Length == 0)
            throw new InvalidOperationException("Буфер має нульову місткість");

        if (_count < _buffer.Length)
        {
            Write(value);
            return;
        }

        // Буфер повний: перезаписуємо найстаріший елемент і зсуваємо head
        _buffer[_head] = value;
        _head = (_head + 1) % _buffer.Length;
    }

    public void Clear()
    {
        Array.Clear(_buffer, 0, _buffer.Length);
        _head = 0;
        _count = 0;
    }
}