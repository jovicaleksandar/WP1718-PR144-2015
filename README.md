# WP1718-PR144-2015
Projekat iz Web programiranja za skolsku 2017/2018. (Web Api)

Taxi Sluzba

Web programiranje – Specifikacija projektnog zadatka
Primenjeno Softversko Inženjerstvo 2018/19
Projektni zadatak

Specifikacija zahteva

Potrebno je realizovati Web aplikaciju za sistem koji podržava taksi službu. Ovu aplikaciju trebaju da
koriste tri grupe (uloge) korisnika: mušterije, dispečer(admini), vozači. Navedeni entiteti su opisani
sledećim podacima:
  
  Korisnik
    • Korisničko ime (jedinstveno)
    • Lozinka
    • Ime
    • Prezime
    • Pol
    • JMBG
    • Kontakt telefon
    • Email
    • Uloga
    • Vožnje (svi tipovi vožnje)
  
  Vozač (Korisnik):
    • Lokacija
    • Automobil
  
  Mušterija, Dispečer (Korisnik)
    Lokacija
      • X i Y koordinate
      • Adresa
    
    Adresa
      • u formatu: Ulica broj, Naseljeno mesto Pozivni broj mesta (npr. Sutjeska 3, Novi Sad 21000)
    
    Vožnja
      • Datum i vreme porudžbine
      • Lokacija na koju taksi dolazi
      • Željeni tip automobila (bez naznake – podrazumevana vrednost, putnički automobil ili kombi
        vozilo)
      • Mušterija za koju je kreirana vožnja (samo ako mušterija inicira kreiranje vožnje)
      • Odredište (lokacija na kojoj je vožnja uspešno završena, za ažuriranje vrednosti odgovoran je
      vozač u trenutku kada se uspešno završi vožnja)
      • Dispečer (ako je formirao ili obradio vožnju, ako je vozač prihvatio onda je ovo polje prazno)
      • Vozač (koji je prihvatio vožnju ili kome je vožnja dodeljena od strane dispečera)
      • Iznos
      • Komentar (opciono se unosi, osim za otkazane vožnje)
      • Status vožnje (Kreirana - Na čekanju, Formirana, Obrađena, Prihvaćena, Otkazana, Neuspešna,
      Uspešna)
    
    Automobil
      • Vozač
      • Godište automobila
      • Broj registarske oznake
      • Broj taksi vozila (jedinstvena oznaka koju svako vozilo ima u okviru svoje taksi službe)
      • Tip automobila (taksi služba raspolaže sa putničkim automobilima i kombi vozilima)
    
    Komentar
      • Opis
      • Datum objave
      • Korisnik koji je ostavio komentar
      • Vožnja na koju je komentar ostavljen
      • Ocena vožnje (vrednost od 1 do 5, 0 vrednost se tumači tako kao da mušterija nije ocenila vožnju)
  
  Statusi vožnje, promena statusa i funkcionalnost aplikacije pri promeni statusa:
      o Kreirana - Na čekanju – Inicijalni status vožnje kada je kreira mušterija
      o Otkazana – vožnja koja je bila u statusu Kreirana - Na čekanju pa ju je mušterija otkazala 
      iz nekog razloga.
      o Formirana – Inicijalni status vožnje kada je kreira dispečer.
      o Obrađena – vožnja koja je bila u statusu Kreirana - Na čekanju pa ju je dispečer obradio
      i dodelio joj je vozača.
      o Prihvaćena– vožnja koja je bila u statusu Kreirana - Na čekanju pa ju je vozač
      samoinicijativno preuzeo.
      o Neuspešna - vožnja koja je bila u statusu Formirana, Obrađena ili Prihvaćena, vozač je
      za nju nije uspešno prevezao mušteriju (npr. tokom vožnje se pokvario automobil, ili
      mušterija nije ušla u taksi iz nekog razloga,...).
      o Uspešna - vožnja koja je bila u statusu Formirana, Obrađena ili Prihvaćena, vozač je za
      nju uspešno prevezao mušteriju.
      Implementirati sledeće funkcionalnosti:
  
  • Registracija – neregistrovani korisnik se registruje na aplikaciju popunjavajući polja koja su za
      to predviđena i nakon toga postaje mušterija.
      • Administratori (Dispečeri) se programski učitavaju iz tekstualnog fajla i ne mogu se naknadno
      dodati. Vozače mogu kreirati samo administratori.
  
  • Prijava na sistem – neprijavljeni korisnik mora da se prijavi na sistem tako što unosi korisničko
    ime i lozinku korisnika za koju je registrovan. Nakon toga, korisnik je prijavljen i može da
    izvršava aktivnosti predviđene njegovom ulogom.

  • Svi korisnici mogu da vide svoje profile i da menjaju svoje lične podatke.

  • Vozač može da promeni svoju trenutnu lokaciju.

  • Mušterija može samostalno da zahteva vožnju, prilikom čega se popunjava trenutna lokacija i
    opciono se popunjava željeni tip automobila. Podrazumevani status vožnje je “Kreirana - Na
    čekanju“.

  • Mušterija može da izmeni ili da odustane (status vožnje „Otkazana“) od svoje vožnje sve dok se
    nalazi u stanju „Kreirana - Na čekanju“. Ako mušterija otkaže vožnju tada se mušteriji obavezno
    otvara forma da popuni komentar za vožnju. Otkaz vožnje je nedostupna funkcionalnost za
    vožnju ukoliko vožnju formira dispečer.

  • Administrator (Dispečer) ima mogućnost formiranja novih vožnji i mogućnost obrade vožnji (npr.
    ukoliko je porudžbina stigla putem telefonskog poziva ili SMS-a).
    o Kada dispečer formira vožnju tada se za vožnju definišu podaci lokacija na koju dolazi
    taksi, opciono se definiše željeni tip automobila, vožnji se obavezno dodeljuje vozač iz
    liste vozača koji nisu zauzeti i postavlja joj se dispečer koji je tu vožnju formirao, a
    mušterija se ne definiše.
    o Ukoliko mušterija inicira vožnju i ukoliko neko od vozača nije preuzeo vožnju, dispečer
    može da dodeli vožnju nekom od slobodnih vozača. Tada se za dispečera inicirane vožnje
    postavlja dispečer koji je obradio prethodnu vožnju.

  • Vozač ima mogućnost da promeni status vožnje za koju je zadužen na status Neuspešna ili uspešna.
    o Ukoliko vozač promeni status vožnje u Neuspešna, podaci za Odredište i Iznos vožnje se 
    ne unose. Po promeni statusa vožnje u Neuspešna vozač mora da postavi komentar.
    Osnovni podaci o vožnji više ne mogu da se menjaju kada dospe u ovaj status.
    o Ukoliko vozač promeni status vožnje na Uspešna, podaci za Odredište i Iznos vožnje se
    unose. Ako za vožnju postoji definisana mušterija, po završetku vožnje mušterija može
    da postavi komentar za vožnju. Osnovni podaci o vožnji više ne mogu da se menjaju kada
    dospe u ovaj status.

  • Prilikom prikaza vožnje prikazuju se (pored svih dozvoljenih informacija) komentar na kome se
  vidi korisničko ime, tekst komentara, ocena vožnje i datum objave.

  • Mušterija na početnoj stranici vidi samo svoje vožnje.
  
  • Dispečeri na početnoj stranici vide spisak vožnji na kojima se oni nalaze. Takođe, omogućen im
    je prikaz svih vožnji u sistemu.

  • Vozači na početnoj stranici vide spisak vožnji na kojima se oni nalaze. Takođe, omogućen im je
    prikaz svih vožnji u sistemu koje su statusu kreirane – na čekanju.

  • Filtriranje - Korisnik može odabrati filtriranje vožnji po Statusu vožnje.

  • Sortiranje – Korisnik može odabrati sortiranje po:
    o Datumu (Najnoviji)
    o Oceni (od najveće ka najmanjoj)

  • Pretraga – Korisnik može pretražiti vožnje po:
    o Datumu porudžbine (od, do, od-do)
    o Oceni (od, do, od-do)
    o Ceni (od, do, od-do)

  • Pretraga vožnji koja je dostupna samo za Dispečere
    o Imenu i/ili prezimenu vozača
    o Imenu i/ili prezimenu mušterije

*** U zavisnosti od konkretne implementacije, studenti mogu proizvoljno organizovati date
entitete ili dodavati druge. ***
Podaci o svim entitetima se čuvaju u tekstualnim datotekama koje mogu biti u proizvoljnom formatu.
Za zadate datoteke, potrebno je ručno implementirati metode za čitanje i pisanje podataka. Za podatke
koji unapred imaju predefinisan skup mogućih vrednosti (pol korisnika, tip automobila, Status…)
potrebno je implementirati odgovarajuće enumeracije.

Dodatni zadatak
  Prilikom odabira lokacije koristiti OpenLayers mape (http://openlayers.org/) ili neku alternativu
  (Google Mape se plaćaju..).
  Administratori (Dispečeri) imaju prava blokiranja mušterija/vozača, čime onemogućuju njihovu dalju
  aktivnost. Takođe mogu ukinuti blokadu.
  Prilikom odabira vozača dispečer ima u ponudi samo pet najbližih vozača u zavisnosti od lokacije
  mušterije. Vozači imaju mogućnost sortiranja vožnje po udaljenosti od njihove trenutne lokacije. Za
  računanje udaljenosti koristiti apsolutno rastojanje između dve tačke u koordinatnoj ravni.
