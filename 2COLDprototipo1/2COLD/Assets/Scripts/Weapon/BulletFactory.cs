using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : AbstractFactory<Bullet>
{
    public BulletFactory(Bullet productToProduce) : base(productToProduce)
    {
    }

    public override Bullet CreateProduct()
    {
        return (Bullet)product.Clone();
    }
}