# IndividuelltProjekt
An online banking application as a project for school.

The flow of the program is pretty simple, we first ask the user to log in, then we fetch an array of all the bank accounts and an integer of how many bank accounts the user has, then we run the FunctionChoice method inside an infinite loop where the user selects which function of the program they want to use. If user chooses to log out, we run the UserLogin method again and the stored userName and passWord are updated along with the integer of how many bank account the user has. What each method does is mostly self-explanatory and is otherwise explained through my comments inside the code.

# Reflektion
Det svåraste i det här projektet för mig var att komma fram till hur jag skulle strukturera koden och de olika metoderna. I början så valde jag att endast ha UserLogin och FunctionChoice i main. FunctionChoice metoden kallade då på TransferMoney och WithdrawMoney metoderna innuti sig men variable scope blev ett rätt stort problem när jag behövde uppdatera värden innuti alla metoder som redan var innuti andra metoder varje gång användaren uppdaterade mängden pengar i sina bankkonton. Jag valde därför att kalla på de primära metoderna innut main där jag gjorde en switch och ändrade så FunctionChoice metoden returnerade ett värde som vi kunde använda i main i stället för att köra metoderna innuti FunctionChoice. Alla metoder undergick ändringar efter det men själva strukturen av programmet var mer eller mindre samma då som det är nu och jag tycker det är en bra lösning på problemet med variable scope och det blev mycket lättare att strukturera programmet efteråt.

Jag valde i början att ha en tom 2d array som skulle innehålla alla bankkonton som sedan uppdaterades beroende på vilken användare det var som loggade in. Denna arrayen hade två kolumner, ett för namnet på själva bankkontona och ett annat för mängden pengar innuti. Detta fungerade men blev ett problem då alla ändringar en användare hade gjort i sina konton blev återställda till default varje gång de loggade ut och in igen. Jag löste detta genom att i stället lagra alla användares bankkonton i en stor 2d array. En stor del av koden i programmet behövde skrivas om efter detta och jag la även in en ny kolumn i arrayen på index 0, användarnamnet av varje kontos ägare. Detta använde jag för att endast välja den inloggade användarens bankkonton och inte alla de andras när programmet visar, tar ut och överför pengar. Nu blev användarens konton inte återställda när de loggade ut och in igen vilket löste problemet och jag tror klasser hade varit lättare men det fungerar och är en ganska bra lösning på problemet.

Jag har kommit på små förbättringar av koden hela tiden så klart, och man kan alltid göra koden bättre. Efter jag såg på Annas presentation om felsökning att TryParse var mer effektivt än Try-Catch så ändrade jag hur input handling fungerar genom hela programmet, och därefter så tänkte jag att jag kunde hantera input fel med en bool i stället för en int och att jag inte behövde ha mer än ett värde för att lagra mängden pengar i ett bankkonto när man ska överföra/ta ut pengar, osv osv. Jag tror hela koden över lag kunde förbättras med använding av klasser, till exempel genom att skapa instanser av en bank-klass för varje användare där man kan ha en array för deras bankkonton i stället för att lägga allas konton innuti en enda stor array. Det hade då blivit lättare att både hantera och ändra mängden pengar i varje konto och man hade slippt loopa igenom hela bankkonto arrayen till man hittar rätt användarnam varje gång man vill göra någon ändring. Dessutom så hade det blivit lättare med variable scope vilket jag hade problem med i början och att kalla metoder. Det är även möjligt att mitt sätt av input/error hantering skulle kunna göras på ett bättre sätt, då jag har en del if satsar innuti varandra men jag kan inte komma på något sätt att förbättra det just nu. Jag tycker mina lösningar är rätt rimliga men, så klart, så finns det alltid förbättringar man kan göra.
