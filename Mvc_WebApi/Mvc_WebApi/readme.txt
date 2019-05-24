- A WebApi is a webservice that is used for working with data. It is made in an MvC project,
	but it does not have a user interface, because a WebApi will 'live' online. If we want to use
	it, we will need to make contact with it in our own programs.


- ValuesController is where all the logic is.
	It is added automatically by Visual Studio when you make a WebAPI project.
	ValuesController inherits from ApiController, not Controller!

- We don't use views in a WebAPI application.
	To test controllers we can use Postman: https://www.getpostman.com.
	This will allow us to make requests to the WebAPI and see what it sends us back.
	(every webapi has its own port number, in this case 50070)
	For example we send a GET request to localhost:50070/api/values and it will send us back an array of strings,
	because the first method in ValuesController is called Get(), and it returns an array of 2 strings.
	
	The WebApi must be started in Visual Studio, or Postman cannot find it! 


- We can add our own controllers: Add -> New Controller -> Web API 2 Controller, empty
	We make a BooksController. We will use this controller to read and change a list of books.
	
	In Models, we add a new class called Book with some properties.
	We will be making a list of books. We will add new books, update books and delete books. (CRUD)

	Because we are not using databases yet in this WebApi, we will keep it simple and just make a List<Book> in
	BooksController, and fill it with a few example books. We make this list STATIC. If it is not static, it will not
	remember any changes we make to it (because everytime we call the method with Postman, it will make a new BooksController,
	which will also make a fresh Books list).


GET METHODS
***********

Now we will make the GET methods, these are methods that only read data and return it, but not change it.

- We make a method that will return a list of books with the GET HTTP Method. In BooksController, this method is called Get()
	and marked with the [HttpGet] attribute. To get this list in Postman, we make a GET request in Postman to 
	localhost:50070/api/Books

- We also make a method that will return only one book, with the Id we specify. This is the method Get(int id) in BooksController.
	in Postman, we can make a GET request to localhost:50070/api/Books/2, this should return the book that has an id of 2

- We also make a method that we can use to search the Books list by author name, this is the method Get(string author).
	Special notation in Postman: p.e. localhost:50070/api/Books?author=veronique

	To avoid having to use this special notation, we can add a Route attribute above the method name: 
	[Route("Api/Books/{author}")]

	then the request looks like: localhost:50070/api/Books/veronique


DELETE METHOD
*************

- The method to delete a book is named Remove and marked with [HttpDelete]. It returns a boolean so we can let a user know
	if the book was deleted or not.
