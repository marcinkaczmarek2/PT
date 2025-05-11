using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Logic.Repositories;
using Logic.Repositories.Interfaces;
using Data.API.Models;
using Data.Enums;

[TestClass]
public class UserRepositoryTest
{
    private class FakeUser : IUser
    {
        public Guid id { get; set; }
        public string name { get; set; } = "";
        public string surname { get; set; } = "";
        public string email { get; set; } = "";
        public string phoneNumber { get; set; } = "";
        public UserRole role { get; set; } = UserRole.Reader;
    }

    [TestMethod]
    public void AddUser_ShouldStoreUser()
    {
        IUserRepository repo = new UserRepository();
        var user = new FakeUser { id = Guid.NewGuid(), email = "test@a.com" };

        repo.AddUser(user);
        var result = repo.GetUser(user.id);

        Assert.IsNotNull(result);
        Assert.AreEqual(user.id, result!.id);
    }

    [TestMethod]
    public void GetUser_NotFound_ReturnsNull()
    {
        IUserRepository repo = new UserRepository();
        var result = repo.GetUser(Guid.NewGuid());
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetAllUsers_ReturnsAllUsers()
    {
        IUserRepository repo = new UserRepository();
        var user1 = new FakeUser { id = Guid.NewGuid() };
        var user2 = new FakeUser { id = Guid.NewGuid() };

        repo.AddUser(user1);
        repo.AddUser(user2);

        var result = repo.GetAllUsers();

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Exists(u => u.id == user1.id));
        Assert.IsTrue(result.Exists(u => u.id == user2.id));
    }

    [TestMethod]
    public void RemoveUser_ExistingUser_ReturnsTrue()
    {
        IUserRepository repo = new UserRepository();
        var user = new FakeUser { id = Guid.NewGuid() };
        repo.AddUser(user);

        var result = repo.RemoveUser(user.id);

        Assert.IsTrue(result);
        Assert.IsNull(repo.GetUser(user.id));
    }

    [TestMethod]
    public void RemoveUser_NonExisting_ReturnsFalse()
    {
        IUserRepository repo = new UserRepository();
        var result = repo.RemoveUser(Guid.NewGuid());

        Assert.IsFalse(result);
    }
}
