﻿namespace Zadanie10.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}