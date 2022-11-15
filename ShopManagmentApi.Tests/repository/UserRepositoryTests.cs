using Moq;
using ShopManagmentAPI.data.db.user;
using ShopManagmentAPI.data.entities;
using ShopManagmentAPI.data.mappers;
using ShopManagmentAPI.data.repository;
using ShopManagmentAPI.domain.model.user;
using ShopManagmentAPI.domain.repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Assert = Xunit.Assert;

namespace ShopManagmentApi.Tests.repository;

public class UserRepositoryTests
{
    private IUserDao userDao;
    private IUserRepository userRepository;
    private User user = new User()
    {
        Email = "Ala@gmail.com",
        Name = "Ala",
        PasswordHash = "123",
        Role = new UserRole()
        {
            Name = "user"
        }
    };

    [Fact]
    public void Create_ValidUser_AddsUser()
    {
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.Create(It.IsAny<UserEntity>()))
            .Returns(UserMapper.ModelToEntity(user));
        userRepository = new UserRepository(userDaoMock.Object);

        var userInDb = userRepository.Create(user);

        Assert.Equal(user.Email, userInDb.Email);
        Assert.Equal(user.Name, userInDb.Name);
        Assert.Equal(user.PasswordHash, userInDb.PasswordHash);
        Assert.Equal(user.Role.Name, userInDb.Role.Name);
    }

    [Fact]
    public void Create_UserTwiceWithTheSameEmail_ThrowsArgumentException()
    {
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.Create(It.IsAny<UserEntity>()))
            .Returns(UserMapper.ModelToEntity(user));

        userRepository = new UserRepository(userDaoMock.Object);

        userRepository.Create(user);

        userDaoMock.Setup(m => m.Create(It.IsAny<UserEntity>()))
            .Throws<ArgumentException>();

        Action action = () => userRepository.Create(user);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Update_NonExistingUser_ThowsArgumentException()
    {
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.Update(It.IsAny<string>(),It.IsAny<UserEntity>()))
            .Throws<ArgumentException>();

        userRepository = new UserRepository(userDaoMock.Object);

        Action action = () => userRepository.Update(user.Email, user);

        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Delete_ExisitngUser_DeletsItFromDb()
    {
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.Create(It.IsAny<UserEntity>()))
            .Returns(UserMapper.ModelToEntity(user));
        userDaoMock.Setup(m => m.Delete(It.IsAny<string>()))
            .Returns(true);
        userRepository = new UserRepository(userDaoMock.Object);

        userRepository.Create(user);
        var isDeleteSuccessful = userRepository.Delete(user.Email);

        Assert.True(isDeleteSuccessful);
    }

    [Fact]
    public void Delete_NonExistingUser_ThrowsArgumentException()
    {
        var userDaoMock = new Mock<IUserDao>();
        userDaoMock.Setup(m => m.Delete(It.IsAny<string>()))
            .Throws<ArgumentException>();
        userRepository = new UserRepository(userDaoMock.Object);

        Action action = () => userRepository.Delete(user.Email);

        Assert.Throws<ArgumentException>(action);
    }
}
