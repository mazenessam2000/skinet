using System;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
        : base(null)
    {
        AddSelect(p => p.Brand);
        ApplyDistinct();
    }
}