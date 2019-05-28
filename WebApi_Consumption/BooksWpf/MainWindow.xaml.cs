using DataLayer;
using DataLayer.Models;
using OrchestrationLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BooksWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BooksService bookService = new BooksService("http://localhost:51518/");

        public MainWindow()
        {
            InitializeComponent();

            cbAuthors.DisplayMemberPath = "Name";
            cbBooks.DisplayMemberPath = "Name";
            cbAuthors.SelectedValuePath = "Id";
            cbBooks.SelectedValuePath = "Id";

            UpdateAuthorComboBox();
        }

        private async Task UpdateAuthorComboBox()
        {
            //cbBooks.ItemsSource = books.GetAll().ToList();
            //cbAuthors.ItemsSource = authors.GetAll().Include(x => x.Books).ToList();

            cbAuthors.ItemsSource = await bookService.GetAuthors();
        }

        #region Event Handlers


        private async void btnNewBook_Click(object sender, RoutedEventArgs e)
        {
            var newBookName = txtNewBook.Text;
            var author = (Author)cbAuthors.SelectedItem;
            var response = await bookService.CreateNewBookWithAuthor(author, newBookName);

            if (response.IsSuccessStatusCode)
            {
                txtNewBook.Text = String.Empty;
                MessageBox.Show("Book successfully added!");
                await UpdateAuthorComboBox();
            }

            else
            {
                MessageBox.Show(response.ReasonPhrase, "Error");
            }

        }


        private async void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewAuthor.Text == String.Empty) { return; }
            try
            {
                var response = await bookService.CreateAuthor(txtNewAuthor.Text);
                if (response.IsSuccessStatusCode)
                {
                    await UpdateAuthorComboBox();
                    txtNewAuthor.Text = String.Empty;
                }

                else
                {
                    MessageBox.Show(response.ReasonPhrase);
                }
            }
            catch (HttpRequestException requestException) { MessageBox.Show(requestException.InnerException.Message, "Error"); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }

        }

        private void cbAuthors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var senderCb = sender as ComboBox;
            if (senderCb.SelectedIndex == -1)
            {
                return;
            }

            cbBooks.ItemsSource = (bookService.GetBooks().Result).Where(x => x.AuthorId == (int)senderCb.SelectedValue);
        }
        #endregion

    }
}
