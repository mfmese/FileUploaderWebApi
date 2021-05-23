# FileUploaderWebApi

## Development Environment
Project created by using .Net5.0 version. Layered architecture used for seperation of concern principle. Application layer used for business concerns, domain layer used for creating domain models,
infrastructure layer used for db connections or other external connections, webapi layer used for presenting data to the outside, and test layer used for unit tests.

## Configuration
For database connection, you must configure the connectionstring in the appsettings.json file in the WebApi project.
You can also change allowed fileTypes and fileSize configuraiton. 
Webapi also currently run by using http://localhost:3000 url. This is important because FileUploaderAngular project connected to this url as default.

## Run Application
#### After setup configuration file, you can create tables in your database by running codes sequentially. 
##### (1) Right click on the "FileUploader.WebApi" project in solution explorer and select "Set as Startup Project"
##### (2) Open package manager console in visual studio
##### (3) select "FileUploader.Infrastructure" from default project part.
##### (4) type PM> add-migration initial
##### (5) click enter, you will see a created migration file
##### (6) type PM> update-database
##### (7) click enter, you will see new created tables which are FileInfos table and ApplicationLogs table
#### After you setup your database, you can run the application and test GetAllFiles and UploadFile endpoints by using swagger
## Test Cases (2 test case created)
#### Not: TestUtility class created for getting appsettings configuraition. Database connection also mocked in order not to connect.
##### (1) ToUploadFile_ShouldCreateFile_WhenFileValidated (for positive scenario)
##### (2) ToUploadFile_ShouldNotCreateFile_WhenFileTypeIsNotValidated (for negative scenario)



