namespace AdaSoftLibrary.Domain.Constants;

public static class MessageConstants
{
    public const string UserNameCannotBeEmpty = "Použivateľské meno je povinné";
    public const string PasswordCannotBeEmpty = "Heslo je povinné";
    public const string IdCannotBeEmpty = "ID je povinné";
    public const string IdMustBeGreatherThanZero = "ID musí byť väčšie ako 0";
    public const string AuthorCannotBeEmpty = "Autor je povinný";
    public const string AuthorCannotExceed15Char = "Autor nesmie presiahnuť 15 znakov"; // 250 znakov
    public const string NameCannotBeEmpty = "Názov je povinný";
    public const string NameCannotExceed15Char = "Názov nesmie presiahnuť 15 znakov";  // 1000 znakov
    public const string YearOutOfRange = "Rok musí byť v rozsahu 1900 až 2100";
    public const string FirstNameCannotBeEmpty = "Meno je povinné";
    public const string FirstNameOutOfRange = "Meno musí byť v rozsahu 3 až 100 znakov";
    public const string LastNameCannotBeEmpty = "Priezvisko je povinné";
    public const string LastNameOutOfRange = "Priezvisko musí byť v rozsahu 3 až 100 znakov";
    public const string FromDateCannotBeEmpty = "Dátum požičania je povinný";
    public const string FromDateCurrentOrInPast = "Dátum požičania nemôže byť v budúcnosti";
    public const string BookCannotBeBorrowed = "Kniha je už požičaná";
    public const string BookMustBeBorrowed = "Kniha nie je požičaná";
    public const string AuthorAndNameMustBeUnique = "Autor a názov knihy musí byť unikátný";
}
