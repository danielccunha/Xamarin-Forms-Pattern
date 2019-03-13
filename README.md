# Xamarin.Forms Patterns
Coding patterns for developing Xamarin.Forms applications. This document was developed based on [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/Mobile), and its purpose is to create a template for any Xamarin.Forms project. The project is basically a **MVVM** project with **Repository Pattern** implemented for local storage (**EF4SQLite**).

### TODOS
- [ ] Create a README explaning the pattern
- [ ] Create a sample project

## Structure
- CommonResources 
  - Fonts
  - Images
  - ...
- Tests
	- [YourProject].UnitTests
		- Behaviors
		- Mocks
		- Services
		- ViewModels
		- ...
	- [YourProject].UITests
	- ...
- [YourProject].Core
	- Behaviors
		- Base
			- BindableBehavior
		- EventToCommandBehavior
	- Controls/Components
	- Converters
	- Exceptions
	- Extensions
	- Helpers
	- Models
	- Services
		- Common
		- Dependency
		- Dialog		
		- Persistance
			- Core
			- Implementation
		- Settings
		- ...
	- Validations
	- ViewModels
		- Base
			- BindableBase
			- MessageKeys
			- ViewModelBase
			- ViewModelLocator
		- ...
	- Views
		- Templates
		- ...
- [YourProject].Droid
	- Activities
	- Assets
	- Renderers
	- Resources
	- Services
- [YourProject].iOS
	- Renderers
	- Services

## Packages
- Xamarin.Essentials
- NUnit
- Microsoft.EntityFrameworkCore.Sqlite

## References
- [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
- [Entity Framework Core 2.0](http://www.macoratti.net/18/08/xf_efcore1.htm)

## License
MIT License.
