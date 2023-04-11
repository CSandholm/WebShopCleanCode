# WebShopCleanCode

Builder design pattern används för att skapa customers.
Proxy används för att hålla produktnamn i en lista och endast kalla på hela objekten när dem faktiskt används.
Databasen är byggd efter singleton då endast en enda instans av klassen ska finnas.

State design pattern används för att styra vilka menyer man är i. Det finns en mapp med klasser som också andvänder state men endast för delegates.
Tanken var att kombinera med andra klasser men jag startade arbetet på inlämningen lite för sent för mitt eget bästa.

Command design pattern används för att styra input och bestäma vart man hamnar.

Jag blev inte helt klar på om man skulle få bort alla switch eller else-if loopar eller bara de i Run(). 
"Ni har implementerat Design Patterns specifikt för att bli av med alla Switch Case eller Else If metoder som just nu används beroende på vilken meny som man är i".
Jag tolkade det först som att det viktiga var att få bort switch och looparna i Run(), så resten av arbetet blev bakom om tanken är att man ska ersätta
samtliga switch och/eller loopar.

Igen ber jag om ursäkt på förväg för dåliga funktion och variabelnamn här och där. Det brukar börja bra sedan blir det plötsligt en enda soppa. Jag har ändå försökt att
hålla det någorlunda förståeligt och bytt en hel del namn.
