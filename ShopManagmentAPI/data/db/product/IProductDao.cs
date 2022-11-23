﻿using ShopManagmentAPI.data.entities;

namespace ShopManagmentAPI.data.db.product;

public interface IProductDao
{
    public void Create(ProductEntity product);
    public bool Update(ProductEntity product);
    public bool Delete(int id);
}