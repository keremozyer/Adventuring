# Running The Application
Configure your local Docker containers to run on Linux.
Open terminal in the project root directory and type
> docker-compose up --build

Thats all, depending on the machine and internet connection this operation may take ~5 minutes.

# Using The Application
Navigate To **http://localhost:8000/swagger/index.html** address to launch Gateway's Swagger UI.
Gateway's Swagger UI defaults to the UserManager microservice and the active microservice can be changed from the combobox in the top-right corner.

# Authentication
All APIs in the AdventureManager and all APIs except POST UserManager/AppUser are locked behind authentication requirements and some require a special role to be accessed.
When the application starts it will create the users defined in the *\Source\Contexts\UserManager\Web\API\Configurations\DataSeedSettings.Development.json* file. **AdminUser** can be used to get a token from *POST /UserManager/Token* service and freely browse all APIs.
AdventureManager microservice uses the token created from the UserManager microservice but Gateway's Swagger UI does not share authentication token between microservice definition UIs. You would get a 401 Unauthorized result if you were to use AdventureManager APIs without using your token to authenticate in that definition's UI.

# Playing The Game
In which order the services should be called and how the game is played is described in the workflow images at the *Docs/Workflows* folder.
SwaggerUI is used to document services, there is no external service documentation. Each service and every request/response property has it's own description on what it does.