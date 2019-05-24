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
	[Route("Api/Books/FindByAuthor/{author}")]

	then the request looks like: localhost:50070/api/Books/FindByAuthor/veronique


DELETE METHOD
*************

- The method to delete a book is named Remove and marked with [HttpDelete]. It returns a boolean so we can let a user know
	if the book was deleted or not. We send a DELETE request to Postman: localhost:50070/api/Books/2
	This will delete the book with Id 2 from the list, if it exists. It will return true or false.
	
POST METHOD (CREATE NEW BOOK)
*****************************

- To add a book to the list, we write a method that takes a Book, adds it to the list and returns true if it worked:
	
	public bool AddNewBook(Book book)

	To test this method in Postman, we will have to take a few extra steps:
	We will send an object in JSON (JavaScript Object Notation) format to our method, and it will be smart enough
	to transform it into a Book object.

	We will make a POST request to localhost:50070/api/Books, but don't click Send yet.
	We click on Body -> raw. 
	In the dropdown on the right, change the type from 'Text' to 'JSON (application/json)'.

	In the textbox we type the following:
	{
        "Id": 5,
        "Title": "Residencia en la Tierra",
        "Author": "Pablo Neruda",
        "PublicationYear": 1935,
        "IsAvailable": true,
        "CallNumber": "PNResidencia"
    }

	Then if we hit Send, we should get 'true' and a new book should be added to our list!

	We CAN use a Route attribute to change the route (like [Route("api/Books/AddNewBook")]) but it is not required.

PUT METHOD (UPDATE A BOOK)
**************************

- To update a book (change title or author etc.) we write a method called UpdateBook marked with [HttpPut]: 
		
	public List<Book> UpdateBook(int id, Book book)
	
	This method takes an int and a Book object and gives back a List<Book>. We will use the simple method of updating
	a book: we will delete the book first, and put the 'new' book in its place.

	To test this in Postman, we do the following:

	We want to update the book with id 1

	- PUT request to localhost:50070/api/Books/1
	- In the body, we put the following:
		{
    	"Id": 1,
    	"Title": "Crepusculario",
    	"Author": "Pablo Neruda",
    	"PublicationYear": 1923,
    	"IsAvailable": true,
    	"CallNumber": "PNCrepusculario"
	}

	this will delete the book with id 1 and put this book in its place



USING SWASHBUCKLE
*****************

To show a nice list of the methods in our WebApi, we can install a NuGet package called Swashbuckle. 
Once it is installed, we can start our WebApi and go to localhost:50070/Swagger
We can also use this to test all of our methods.



