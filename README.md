Workshop project working with existing code, cleaning it and implementing designpatters.

# WebShopCleanCode

Builder design pattern används för att skapa customers.
Proxy används för att hålla produktnamn i en lista och endast kalla på hela objekten när dem faktiskt används.
Databasen är byggd efter singleton då endast en enda instans av klassen ska finnas.

State design pattern används för att styra vilka menyer man är i. Det finns en mapp med klasser som också andvänder state men endast för delegates.

Command design pattern används för att styra input och bestäma vart man hamnar.
