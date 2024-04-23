using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory<T> where T : IProduct
{
    public AbstractFactory(T productToProduce)
    {
        product = productToProduce;
    }

    protected T product;
    public abstract T CreateProduct();
}

public interface IProduct
{
    IProduct Clone();
    GameObject MyGameObject { get; }
}