Link koji dodatno objasnjava kako se moze kontrolisati na sta se preslikavaju klase prilikom nasledjivanja:
http://www.entityframeworktutorial.net/code-first/inheritance-strategy-in-code-first.aspx

# /token

/token je endpoint koji koristimo da bi dobili validan token koji predstavlja autentifikovanog korisnika.

Kako kontrolisemo koje ce dodatne informacije biti prikazane u odgovoru. To se postize tako sto overridujemo metodu `TokenEndpoint` u klasi `SimpleAuthorizationServerProvider`.
U ovom slucaju zelimo da dodamo informacije o emailu, rolama i korisnickom imenu u response /token endpoint-a. Sve te informacije su za logovanog korisnika vec dodate
u kontekst kao objekti `Claim` klase. Tako da samo treba da sve informacije iz `Claim`-ova prebacimo u response.