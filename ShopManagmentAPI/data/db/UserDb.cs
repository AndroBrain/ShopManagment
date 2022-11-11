using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model;
using System.Numerics;

namespace ShopManagmentAPI.data.db;

public class UserDb
{
    private readonly static Dictionary<int, UserEntity> _users = new();

    public List<UserEntity> GetAll()
    {
        return _users.Values.ToList();
    }

    public UserEntity? Get(int id)
    {
        return _users.TryGetValue(id, out UserEntity? value) ? value : null;
    }
    public UserEntity Add(UserEntity user)
    {
        _users.Add(user.Id, user);
        return _users[user.Id];
    }

    public UserEntity Update(UserEntity user)
    {
        _users[user.Id] = user;
        return user;
    }

    public bool Remove(int id)
    {
        return _users.Remove(id);
    }
}
