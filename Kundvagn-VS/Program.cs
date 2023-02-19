// See https://aka.ms/new-console-template for more information
using Kundvagn_VS;

ShoppingCart<Item> vagn = new ShoppingCart<Item>();

vagn.Add(new Item("Banan", 10, "Gula"));
vagn.PrintStorage("En insättning", false);
vagn.Add(new Item("Apelsin", 20, "Gula"));
vagn.PrintStorage("En insättning", false);
vagn.Add(new Item("Morot", 30, "Orange"));
vagn.Add(new Item("Mjölk", 40, "Orange"));
vagn.Add(new Item("Mjölk", 50, "Vit"));
vagn.PrintStorage("Tre insättning med moms", true);
foreach (var posten in vagn)
{
  Console.WriteLine(posten);
}

vagn.Clear();
vagn.PrintStorage("Tom kundvagn", false);
vagn.Add(new Item("Mjölk", 60, "Vit"));
vagn.Add(new Item("Fil", 10, "Laktos"));
vagn.PrintStorage("Nu är listan :", false);

vagn.Remove("Mjölk");
vagn.PrintStorage("En sak borttaget:", false);
foreach (var posten in vagn)
{
  Console.WriteLine(posten);
}
vagn.RemoveElementAt(1);
vagn.PrintStorage("En plats borttaget:", false);

vagn.Add(new Item("Gurka", 10, "Grön"));
vagn.PrintStorage("En sak pluss:", false);
foreach (var posten in vagn)
{
  Console.WriteLine(posten);
}
Console.ReadLine();