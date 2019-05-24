- ValuesController is where all the logic is.
	It is added automatically by Visual Studio when you make a WebAPI project.
	ValuesController inherits from ApiController, not Controller!

- We don't use views in a WebAPI application.
	To test controllers we can use Postman: https://www.getpostman.com.
	This will allow us to make requests to the WebAPI and see what it sends us back.
	(every webapi has its own port number, in this case 50070)
	For example we send a GET request to localhost:50070/api/values and it will send us an array of strings.
	The WebApi must be running! 



