using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.domain.model;
using System.Numerics;

namespace ShopManagmentAPI.data.db.user;

public class UserDb : IUserDao
{
    private readonly static Dictionary<string, UserEntity> _users = new();

    public List<UserEntity> GetAll()
    {
        return _users.Values.ToList();
    }

    public UserEntity? Get(string email)
    {
        return _users.TryGetValue(email, out UserEntity? value) ? value : null;
    }
    public UserEntity Create(UserEntity user)
    {
        _users.Add(user.Email, user);
        return _users[user.Email];
    }

    public UserEntity Update(UserEntity user)
    {
        if (!_users.ContainsKey(user.Email))
        {
            throw new ArgumentException();
        }
        _users[user.Email] = user;
        return user;
    }

    public bool Delete(string email)
    {
        if (!_users.ContainsKey(email))
        {
            throw new ArgumentException();
        }
        return _users.Remove(email);
    }
}
