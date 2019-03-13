# Xamarin.Forms Patterns
Coding patterns for developing Xamarin.Forms applications. This document was developed based on [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/Mobile), and its purpose is to create a template for any Xamarin.Forms project. The project is basically a **MVVM** project with **Repository Pattern** implemented for local storage (**EF4SQLite**).

### TODOS
- [ ] Create a sample project

## Structure
- Tests
	- [YourProject].UITests
	- [YourProject].UnitTests	
- [YourProject]
	- Assets
	- Behaviors
	- Bootstrap (AppSetup and AppContainer)
	- Constants (MessagingConstants, ApiConstants, etc...)
	- Contracts
		- Persistence
			- Repositories
			- IUnitOfWork
			- ...
		- Services
			- General (INavigationService, ISettingsService, IDialogService, IConnectionService)
			- ...
	- Controls
	- Converters
	- Enumerations
	- Exceptions
	- Extensions
	- Models
	- Persistence
		- Repositories
		- UnitOfWork
		- ...
	- Services (same structure of Contracts/Services)
		- General
		- ...
	- Utility
		- ViewModelLocator
		- ...
	- Validations
	- ViewModels
		- Base
			- BindableBase
			- ViewModelBase
			- ...
		- ...
	- Views
		- Templates
		- ...
- [YourProject].Android
	- Activities
	- Renderers
	- Services
- [YourProject].iOS
	- Renderers
	- Services

## Packages
- AutoFac
- NUnit
- Microsoft.EntityFrameworkCore.Sqlite

## References
- [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
- [BethanysPieShopMobile](https://github.com/GillCleeren/BethanysPieShopMobile)
- [Entity Framework Core 2.0](http://www.macoratti.net/18/08/xf_efcore1.htm)

## License
MIT License.
