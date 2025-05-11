using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Logic.Repositories;
using Logic.Repositories.Interfaces;
using Data.API.Models;

[TestClass]
public class LibraryRepositoryTest
{
    private class FakeItem : IBorrowable
    {
        public Guid id { get; set; }
        public string title { get; set; } = "";
        public string publisher { get; set; } = "";
        public bool availability { get; set; } = true;
    }

    [TestMethod]
    public void AddContent_ShouldStoreItem()
    {
        ILibraryRepository repo = new LibraryRepository();
        var item = new FakeItem { id = Guid.NewGuid(), title = "Book" };

        repo.AddContent(item);
        var stored = repo.GetContent(item.id);

        Assert.IsNotNull(stored);
        Assert.AreEqual(item.id, stored!.id);
    }

    [TestMethod]
    public void GetContent_NotFound_ReturnsNull()
    {
        ILibraryRepository repo = new LibraryRepository();
        var result = repo.GetContent(Guid.NewGuid());
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetAllContent_ReturnsAllItems()
    {
        ILibraryRepository repo = new LibraryRepository();
        var item1 = new FakeItem { id = Guid.NewGuid() };
        var item2 = new FakeItem { id = Guid.NewGuid() };

        repo.AddContent(item1);
        repo.AddContent(item2);

        var result = repo.GetAllContent();

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Exists(i => i.id == item1.id));
        Assert.IsTrue(result.Exists(i => i.id == item2.id));
    }

    [TestMethod]
    public void RemoveContent_ExistingItem_ReturnsTrue()
    {
        ILibraryRepository repo = new LibraryRepository();
        var item = new FakeItem { id = Guid.NewGuid() };
        repo.AddContent(item);

        var result = repo.RemoveContent(item.id);

        Assert.IsTrue(result);
        Assert.IsNull(repo.GetContent(item.id));
    }

    [TestMethod]
    public void RemoveContent_NonExisting_ReturnsFalse()
    {
        ILibraryRepository repo = new LibraryRepository();
        var result = repo.RemoveContent(Guid.NewGuid());

        Assert.IsFalse(result);
    }
}
