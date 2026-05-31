migrate-add:
	dotnet ef migrations add $(name) --project SmartFactory.Data --startup-project FactoryApi

migrate-update:
	dotnet ef database update --project SmartFactory.Data --startup-project FactoryApi

	