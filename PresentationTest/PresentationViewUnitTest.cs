using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;
using PresentationTest;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Test
{
    [TestClass]
    public class PresentationViewUnitTest
    {
        private readonly MockModel _mockModel = new MockModel();

        [TestMethod]
        public async Task BookViewModel_AddAndDeleteBook_WorksCorrectly()
        {
            var viewModel = new VMBookList(_mockModel);
            viewModel.SelectedVM.Clear();

            viewModel.Id = 1;
            viewModel.Title = "Test Book";
            viewModel.Publisher = "Test Publisher";
            viewModel.Author = "Test Author";
            viewModel.NumberOfPages = 123;
            viewModel.Genre = "Fiction";

            await Task.Run(() => viewModel.GetBooks());
            await Task.Run(() => viewModel.AddBook.Execute(null));

            viewModel.GetBooks();
            var added = viewModel.SelectedVM.FirstOrDefault(b => b.Id == 1);

            Assert.IsNotNull(added);
            Assert.AreEqual("Test Book", added.Title);

            viewModel.SelectedBook = (Model.API.IBookModelData)added;
            await Task.Run(() => viewModel.DeleteBook.Execute(null));

            viewModel.GetBooks();
            var removed = viewModel.SelectedVM.FirstOrDefault(b => b.Id == 1);
            Assert.IsNull(removed);
        }
    }
}
