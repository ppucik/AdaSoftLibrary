# AdaSoft Testovací príklad – C#/.NET Developer

Príklad je zameraný na overenie základných programátorských schopností v C# na platforme .NET s použitím vývojového nástroja Microsoft Visual Studio.

## Zadanie

Vytvorte jednoduchú aplikáciu pre správu kníh v knižnici. Prístup k aplikácii by mal byť chránený jednoduchým prihlasovacím procesom (používateľské meno a heslo).

Po prihlásení sa používateľovi zobrazí zoznam kníh a menu so základnými operáciami aplikácie:
- zoznam všetkých kníh,
- zoznam voľných kníh,
- zoznam požičaných kníh,
- založenie novej knihy,
- editácia knihy,
- vypožičanie/vrátenie knihy,
- zmazanie knihy.

Vstupná XML databáza bude uložená v súbore **Library.xml**, dodávaná spoločne s týmto dokumentom (pozri nižšie Príloha č.1. Ak ste viac zástancom dátového formátu JSON, tak môžete dodanú databázu Library.xml transformovať do Library.json s tým, že v maximálnej miere bude zachovaná pôvodná štruktúra dátového súboru. Následne implementujte zadanie s preferovanou vstupnou **Library.json** databázou.

Aplikácia by mala byť vytvorená v 3-vrstvovej architektúre s oddelením prezentačnej (PL), aplikačnej (BL) a dátovej vrstvy (DL) na báze .NET a C#.

Prezentačnú vrstvu (PL) realizujte formou aplikácie, podľa vášho výberu jednej z nasledovných technológií (MVC, WPF (MVVM), Blazor, Angular) na báze .NET a C#. Aplikácia by mala mať implementovanú validáciu vstupných formulárov:
- validácia povinných polí (názov knihy a autor)
- validácia na maximálnu dĺžku názvu knihy na 15 znakov.
- validácia dátumu výpožičky, vypožičaná kniha musí mať vyplnený dátum výpožičky + aby nebolo možné zadať dátum z budúcnosti

Aplikačnú vrstvu (BL) realizujte prostredníctvom webových služieb, podľa vášho výberu, v niektorej z nasledovných technológií (REST API, WCF, ASP.NET (ASMX)) na báze .NET a C#.

> :warning: **Všimnite si:** _V prípade, že sa rozhodnete implementovať prezentačnú vrstvu (PL) v technológii Angular, tak očakávame, že aplikačná vrstva (BL) bude implementovaná, ako REST API na báze .NET a C#._

Dátovým úložiskom (DL) aplikácie je súbor XML (resp. JSON).

> :memo: **Note:** Nie je kladený dôraz na grafický vzhľad aplikácie!!! Ale určite nechceme brániť kreativite.

## Ďalšie požiadavky

Aplikáciu vytvorte v prostredí Visual Studio 2022/2019.

Aplikáciu navrhnite tak, aby ju bolo možné ďalej rozvíjať – napr. v budúcnosti by ste plánovali nahradiť XML súbor (JSON súbor) uložením dát v databáze.
1. Riešenie nemá používať žiadnu databázu (ani jej časť), má byť len nachystané na možnosť zmeny úložiska.
2. Meno, heslo a konfigurácia aplikácie (cesta k XML súboru/JSON, atď.) má byť uložená v konfiguračnom súbore (app.config/web.config/appsettings.json).
3. V aplikačnej vrstve (BL) je potrebné implementovať aj zápis do logovacieho súboru. Logovanie nemusí byť podrobné (stačí v 1 až 2 metódach), ale má slúžiť ako „vzor“, ako by ste to implementovali v celej aplikácii.

> :warning: **Všimnite si:** _Pri implementácií testovacieho príkladu nie je nutné všetko „odkódovať“. Naopak, ak viete použiť pre implementovanie niektorých častí vhodnú existujúcu „knižnicu“, tak to vyhodnotíme ako správne riešenie._

## Spôsob hodnotenia

Hodnotené bude:
- Výber a použitie technológií pre implementáciu PL, BL a DL (**pre všetky vrstvy platí, že preferované sú novšie technológie**).
- Dodržiavanie zásad objektového programovania.
- Zrozumiteľnosť a štruktúrovanosť kódu.
- Efektívne a elegantné postupy.
- Spracovanie chýb.
- Programátorská dokumentácia kódu.
- Použitie existujúcich frameworkov/knižníc.
- Práca s XML, resp. JSON.
- UNIT test pre aplikačnú vrstvu (BL).
- Vlastná pridaná hodnota.

## Príloha č.1

Obsah súboru **Library.xml**

```xml
<?xml version="1.0" encoding="windows-1250"?>
<Library>
	<Book id="1">
		<Name>Starec a more</Name>
		<Author>Ernest Hemingway</Author>
		<Borrowed>
			<FirstName>Peter</FirstName>
			<LastName>Prvý</LastName>
			<From>25.3.2016</From>
		</Borrowed>
	</Book>
	<Book id="2">
		<Name>Rómeo a Júlia</Name>
		<Author>William Shakespeare</Author>
		<Borrowed>
			<FirstName>Lukáš</FirstName>
			<LastName>Druhý</LastName>
			<From>16.6.2018</From>
		</Borrowed>
	</Book>
	<Book id="3">
		<Name>Krvavé sonety</Name>
		<Author>Pavol Országh Hviezdoslav</Author>
		<Borrowed>
			<FirstName>Matej</FirstName>
			<LastName>Tretí</LastName>
			<From>1.2.2017</From>
		</Borrowed>
	</Book>
	<Book id="4">
		<Name>Hájniková žena</Name>
		<Author>Pavol Országh Hviezdoslav</Author>
		<Borrowed>
			<FirstName/>
			<LastName/>
			<From/>
		</Borrowed>
	</Book>
	<Book id="5">
		<Name>Hamlet</Name>
		<Author>William Shakespeare</Author>
		<Borrowed>
			<FirstName>Jozef</FirstName>
			<LastName>Štvrtý</LastName>
			<From>25.10.2009</From>
		</Borrowed>
	</Book>
	<Book id="6">
		<Name>Živý bič</Name>
		<Author>Milo Urban</Author>
		<Borrowed>
			<FirstName/>
			<LastName/>
			<From/>
		</Borrowed>
	</Book>
</Library>
```